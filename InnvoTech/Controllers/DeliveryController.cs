using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InnvoTech.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InnvoTech.Controllers
{
    public class DeliveryController : Controller
    {
        private BobTestContext _context;
        private Braintree.BraintreeGateway _braintreeGateway;

        public DeliveryController(BobTestContext context, Braintree.BraintreeGateway braintreeGateway)
        {
            _context = context;
            _braintreeGateway = braintreeGateway;
        }

        [HttpGet]
        public IActionResult Index()
        {
            string cartId;
            Guid trackingNumber;
            DeliveryViewModel model = new DeliveryViewModel();
            if (Request.Cookies.TryGetValue("cartId", out cartId) && Guid.TryParse(cartId, out trackingNumber) && _context.Cart.Any(x => x.TrackingNumber == trackingNumber))
            {
                var cart = _context.Cart.Include(x => x.CartProducts).ThenInclude(y => y.Products).Single(x => x.TrackingNumber == trackingNumber);
                model.CartProducts = cart.CartProducts.ToArray();
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Update(int quantity, int productId)
        {
            string cartId;
            Guid trackingNumber;
            if (Request.Cookies.TryGetValue("cartId", out cartId) && Guid.TryParse(cartId, out trackingNumber) && _context.Cart.Any(x => x.TrackingNumber == trackingNumber))
            {

                var cart = _context.Cart.Include(x => x.CartProducts).ThenInclude(y => y.Products).Single(x => x.TrackingNumber == trackingNumber);
                var cartItem = cart.CartProducts.Single(x => x.Products.Id == productId);
                cartItem.Quantity = quantity;

                if (cartItem.Quantity == 0)
                {
                    _context.CartProducts.Remove(cartItem);
                }
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(DeliveryViewModel models)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^\d{5}(?:[-\s]\d{4})?$");
            if (string.IsNullOrEmpty(models.ShippingZip) || !regex.IsMatch(models.ShippingZip))
            {
                ModelState.AddModelError("Zip", "The zip code is invalid!");
            }

            if (ModelState.IsValid)
            {
                string cartId;
                Guid trackingNumber;
                if (Request.Cookies.TryGetValue("cartId", out cartId) && Guid.TryParse(cartId, out trackingNumber) && _context.Cart.Any(x => x.TrackingNumber == trackingNumber))
                {

                    var cart = _context.Cart.Include(x => x.CartProducts).ThenInclude(y => y.Products).Single(x => x.TrackingNumber == trackingNumber);

                    Braintree.TransactionRequest saleRequest = new Braintree.TransactionRequest();
                    saleRequest.Amount = cart.CartProducts.Sum(x => x.Products.Price * x.Quantity) ?? .01m;    //Hard-coded for now
                    saleRequest.CreditCard = new Braintree.TransactionCreditCardRequest
                    {
                        CardholderName = models.creditcardname,
                        CVV = models.creditcardverificationvalue,
                        ExpirationMonth = models.expirationmonth,
                        ExpirationYear = models.expirationyear,
                        Number = models.creditcardnumber
                    };
                    saleRequest.BillingAddress = new Braintree.AddressRequest
                    {
                        StreetAddress = models.BillingAddress,
                        PostalCode = models.BillingZip,
                        Region = models.BillingState,
                        Locality = models.BillingCity,
                        CountryName = "United States of America",
                        CountryCodeAlpha2 = "US",
                        CountryCodeAlpha3 = "USA",
                        CountryCodeNumeric = "840"
                    };
                    var result = await _braintreeGateway.Transaction.SaleAsync(saleRequest);
                    if (result.IsSuccess())
                    {
                        return this.RedirectToAction("Index", "Home");
                    }
                    foreach (var error in result.Errors.All())
                    {
                        ModelState.AddModelError(error.Code.ToString(), error.Message);
                    }
                }
            }
                return View();
            }
        }
    }


