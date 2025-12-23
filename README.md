# InnvoTech

An ASP.NET Core e-commerce application built with MVC architecture, featuring user authentication, product catalog, shopping cart, and payment processing.

## Overview

InnvoTech is a modern web application that provides a complete e-commerce platform with user account management, product browsing, shopping cart functionality, and order delivery tracking. The application leverages ASP.NET Core's powerful features combined with Entity Framework Core for data management.

## Features

- **User Authentication & Authorization**: Secure login, registration, and password reset functionality
- **Product Catalog**: Browse and search for gadgets with detailed product information
- **Shopping Cart**: Add products to cart, manage quantities, and proceed to checkout
- **Order Management**: Complete order lifecycle from checkout to delivery
- **Payment Processing**: Integration with Braintree for secure payment handling
- **Email Notifications**: SendGrid integration for automated email communications
- **User Reviews**: Customers can leave reviews for products
- **Delivery Tracking**: Track order delivery status and view receipts
- **Address Validation**: SmartyStreets SDK integration for address validation

## Tech Stack

### Backend
- **Framework**: ASP.NET Core 2.2
- **Language**: C#
- **Database**: SQL Server
- **ORM**: Entity Framework Core
- **Authentication**: ASP.NET Identity

### Frontend
- **Views**: Razor HTML templates
- **Styling**: Bootstrap with custom CSS
- **Validation**: jQuery with unobtrusive validation
- **Scripts**: jQuery, Popper.js

### External Services
- **Payments**: Braintree
- **Email**: SendGrid
- **Address Validation**: SmartyStreets
- **UI Framework**: Bootstrap

## Project Structure

```
InnvoTech/
├── Controllers/          # MVC Controllers
│   ├── AccountController.cs     # User authentication
│   ├── ProductsController.cs    # Product catalog
│   ├── CartController.cs        # Shopping cart
│   ├── DeliveryController.cs    # Order delivery
│   └── HomeController.cs        # Home page
├── Models/               # Data models & ViewModels
│   ├── ApplicationUser.cs       # User identity
│   ├── Products.cs              # Product entity
│   ├── Order.cs                 # Order entity
│   ├── Cart.cs & CartProducts.cs# Shopping cart
│   ├── Review.cs                # Product reviews
│   └── BobTestContext.cs        # DbContext
├── Views/                # Razor view templates
│   ├── Account/         # Login, register, password reset
│   ├── Products/        # Product catalog pages
│   ├── Cart/            # Shopping cart
│   ├── Delivery/        # Order confirmation & tracking
│   └── Shared/          # Layout & shared components
├── Migrations/           # Entity Framework migrations
├── wwwroot/             # Static files
│   ├── css/             # Stylesheets
│   ├── js/              # JavaScript files
│   └── lib/             # Third-party libraries
├── Properties/          # Configuration files
├── Startup.cs           # Service configuration
├── Program.cs           # Application entry point
└── appsettings.json     # Configuration settings
```

## Getting Started

### Prerequisites
- .NET Core 2.2 SDK or later
- SQL Server (LocalDB or full instance)
- Visual Studio 2017+ or Visual Studio Code with C# extension

### Installation

1. **Clone or download the project**
   ```bash
   cd InnvoTech
   ```

2. **Restore NuGet packages**
   ```bash
   dotnet restore
   ```

3. **Configure the database connection**
   - Update the `DefaultConnection` in `appsettings.json` with your SQL Server connection string

4. **Apply database migrations**
   ```bash
   dotnet ef database update
   ```

5. **Configure API keys**
   - Add your Braintree API credentials to `appsettings.json`
   - Add your SendGrid API key to `appsettings.json`
   - Add your SmartyStreets API key configuration

6. **Run the application**
   ```bash
   dotnet run
   ```

The application will start on `https://localhost:5001` (or the configured port in `launchSettings.json`)

## Configuration

### appsettings.json
Main configuration file containing:
- Database connection strings
- Braintree payment gateway credentials
- SendGrid email service credentials
- SmartyStreets address validation settings

### appsettings.Development.json
Development-specific settings that override defaults in `appsettings.json`

## Database

The project uses SQL Server with Entity Framework Core. The `BobTestContext` manages all entity relationships and migrations.

### Key Entities
- **ApplicationUser**: User account information
- **Products**: Product catalog
- **Cart & CartProducts**: Shopping cart management
- **Order & LineItem**: Order management
- **Review**: Product reviews

Run migrations with:
```bash
dotnet ef migrations add MigrationName
dotnet ef database update
```

## API Integrations

### Braintree (Payment Processing)
- Handles secure credit card transactions
- Manages payment methods and transactions
- Configured in DeliveryController for checkout

### SendGrid (Email Service)
- Sends account verification emails
- Password reset communications
- Order confirmations and notifications

### SmartyStreets (Address Validation)
- Validates and standardizes customer addresses
- Ensures delivery accuracy

## Development

### Building
```bash
dotnet build
```

### Running Tests
```bash
dotnet test
```

### Publishing
```bash
dotnet publish -c Release
```

## User Flows

### Registration & Authentication
Users can register with email, set passwords, reset forgotten passwords, and securely log in using ASP.NET Identity.

### Shopping
1. Browse products in the gadgets catalog
2. Add items to shopping cart
3. Review cart and adjust quantities
4. Proceed to checkout with delivery details
5. Validate address using SmartyStreets
6. Process payment via Braintree

### Order Management
- View order history
- Track delivery status
- Download receipts
- Leave product reviews

## Future Enhancements

- Product search and filtering
- Wishlist functionality
- Order cancellation
- Inventory management admin panel
- Analytics and reporting
- Mobile app version

## License

[Add your license information here]

## Contact

For questions or support, please contact [your contact information].

---

**Note**: This project contains sensitive configuration. Always use environment variables or user secrets for API keys and connection strings in production environments.
