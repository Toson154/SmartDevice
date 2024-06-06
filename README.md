# Devices E-Commerce Website

This is an ASP.NET Core MVC project for an e-commerce website selling various electronic devices such as computers, freezers, TVs, etc. The project includes user registration and login functionality without using ASP.NET Identity.

## Features

- User registration and login
- Product categories
- Product listings
- User profiles
- Shopping cart and order processing
- Vendor management
- Review system

## Prerequisites

- [.NET 7.0 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/)

## Setup

### 1. Clone the Repository

git clone https://github.com/Toson154/SmartDevice.git
cd Devices-E-Commerce

### 2. Configure the Database

1. Update the connection string in `appsettings.json` to match your SQL Server configuration:

    "ConnectionStrings": {
        "Devices": "Server=your_server;Database=DevicesDB;Trusted_Connection=True;MultipleActiveResultSets=true"
    }

2. Create the database using Entity Framework Core migrations:

    dotnet ef database update

### 3. Build and Run the Application

1. Build the project:

    dotnet build

2. Run the project:

    dotnet run

### 4. Access the Application

Open your web browser and navigate to `https://localhost:5001` or `http://localhost:5000`.

## Project Structure

- **Controllers**: Contains MVC controllers for handling HTTP requests.
- **Models**: Contains entity classes representing the database schema.
- **Views**: Contains Razor views for rendering HTML.
- **Data**: Contains the `ApplicationDbContext` for EF Core database context.
- **wwwroot**: Contains static files such as CSS, JavaScript, and images.

## Key Classes and Files

- **Program.cs**: Main entry point of the application.
- **ApplicationDbContext.cs**: EF Core database context class.
- **AccountController.cs**: Handles user registration, login, and logout.
- **CustomerAccount.cs**: Represents customer accounts.
- **Order.cs**: Represents orders placed by customers.
- **OrderItem.cs**: Represents individual items within an order.
- **Product.cs**: Represents products available for sale.
- **Category.cs**: Represents product categories.

## Authentication and Authorization

User authentication and registration are handled without using ASP.NET Identity. Instead, a custom implementation is used within the `AccountController`.

## License

This project is licensed under the MIT License. See the LICENSE file for details.
