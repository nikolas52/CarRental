# Car Rental System

## Project Overview
The Car Rental System is a web application built with ASP.NET Core to manage a car rental business. Users can perform CRUD operations on clients, cars, and rentals, with additional features like pagination, client search by last name, and data validation with Polish error messages. The application provides a responsive user interface and ensures a seamless user experience with localized validation.

## Requirements
- .NET 8 SDK (or later)
- Visual Studio 2022 (or another IDE supporting .NET)
- Database (e.g., SQL Server, SQLite) configured in `appsettings.json`
- Installed NuGet packages:
  - `Microsoft.EntityFrameworkCore`
  - `FluentValidation.AspNetCore`
  - `Microsoft.AspNetCore.Mvc.ViewFeatures`

## Installation and Setup
Clone the repository:
```bash
git clone https://github.com/<nikolas52>/car-rental-system.git
cd car-rental-system
```

Configure the database:  
Update the connection string in `appsettings.json` to match your database setup.

Run migrations to create the database:
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

Install dependencies:
```bash
dotnet restore
```

Run the application:
```bash
dotnet run
```
Open your browser and navigate to `https://localhost:5001` (or the port displayed in the console).

## Contributing
Contributions are welcome! To contribute:
1. Fork the repository.
2. Create a new branch (`git checkout -b feature/your-feature`).
3. Make your changes and commit them (`git commit -m "Add your feature"`).
4. Push to your branch (`git push origin feature/your-feature`).
5. Open a Pull Request.

**Note**:  
- Replace `https://github.com/<your-username>/car-rental-system.git` with your actual GitHub repository URL.  
- Update `[Your Name]` and `[your-email@example.com]` with your actual information.  
- If you don't have a `LICENSE` file, you can add one with the MIT License text.
