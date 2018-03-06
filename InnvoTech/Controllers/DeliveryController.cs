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
        public async Task<IActionResult> Index(DeliveryViewModel models, string creditcardnumber, string creditcardname, string creditcardverificationvalue, string expirationmonth, string expirationyear)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^\d{5}(?:[-\s]\d{4})?$");
            if (string.IsNullOrEmpty(models.Zip) || !regex.IsMatch(models.Zip))
            {
                ModelState.AddModelError("Zip", "The zip code is invalid!");
            }

            if (ModelState.IsValid)
            {
                Braintree.TransactionRequest saleRequest = new Braintree.TransactionRequest();
                saleRequest.Amount = 10;
                saleRequest.CreditCard = new Braintree.TransactionCreditCardRequest()
                {
                    CardholderName = creditcardname,
                    CVV = creditcardverificationvalue,
                    ExpirationMonth = expirationmonth,
                    ExpirationYear = expirationyear,
                    Number = creditcardnumber
                };
                var result = await _braintreeGateway.Transaction.SaleAsync(saleRequest);
                if (result.IsSuccess())
                {
                    return this.RedirectToAction("Index", "Home");
                }
                foreach(var error in result.Errors.All())
                {
                    ModelState.AddModelError(error.Code.ToString(), error.Message);
                }
            }

            return View();
        }
    }
}

