# Plant Tracking System MVC

The Plant Tracking System MVC is a comprehensive web application designed to monitor and manage the growth processes of plants. This application allows users to record and track various maintenance activities such as watering, fertilizing, and other care routines for their plants.

## Contents
- [Features](#features)
- [Technologies Used](#technologies-used)
- [Installation](#installation)
- [Usage](#usage)
- [Project Structure](#project-structure)
- [Database Configuration](#database-configuration)
- [API Endpoints](#api-endpoints)
- [Contributing](#contributing)
- [License](#license)

## Features
- User Authentication:** Secure registration and login system.
- Plant Management:** Add, edit, and delete plants.
- Activity Logging:** Record watering, fertilizing, and other maintenance activities.
- Data Visualization:** View and analyze plant growth data.
- User-friendly Interface:** Intuitive and easy-to-use interface for efficient navigation.

## Technologies Used
- Backend: ASP.NET Core MVC
- Frontend: HTML, CSS, JavaScript
- Database: SQL Server
- ORM: Entity Framework Core
- Authentication: ASP.NET Identity

## Installation

### Requirements
- .NET SDK
- Visual Studio or a similar IDE
- SQL Server

### Steps
1. Clone the repository:
    ```bash
    git clone https://github.com/aozgokmen/BitkiTakipSystemMVC.git
    ```
2. Open the project in Visual Studio:
    ```bash
    cd BitkiTakipSystemMVC/BitkiTakipSystemMVC
    ```
3. Create a new SQL Server database.
4. Update the connection string in `appsettings.json` to match your SQL Server configuration:
    ```json
    "ConnectionStrings": {
        "DefaultConnection": "Server=YOUR_SERVER;Database=YOUR_DATABASE;User Id=YOUR_USERNAME;Password=YOUR_PASSWORD;"
    }
    ```
5. Apply database migrations to set up the database schema:
    ```bash
    Update-Database
    ```
6. Run the application:
    ```bash
    dotnet run
    ```

## Usage
1. Register or Login:**
   - Open the application in your browser.
   - Register a new account or log in with an existing account.
   
2. Manage Plants:
   - Navigate to the "My Plants" section.
   - Add new plants, edit existing ones, or delete plants.
   
3. Log Activities:
   - For each plant, record activities such as watering and fertilizing.
   
4. View Reports:
   - Access the "Reports" section to view and analyze the growth data of your plants.

## Project Structure
- Controllers: Contains MVC controllers managing the application's flow.
- Models: Contains data models representing the application’s data structure.
- Views: Contains Razor views for the user interface.
- Data: Contains database context and migration files.
- wwwroot: Contains static files such as CSS, JavaScript, and images.

## Database Configuration
Ensure that the connection string in `appsettings.json` is correctly configured for your SQL Server instance. The database schema is managed through Entity Framework Core migrations. 

## API Endpoints
The application provides several API endpoints for managing plants and activities:
- GET /plants:** Retrieve a list of all plants.
- POST /plants:** Add a new plant.
- PUT /plants/{id}:** Update a specific plant.
- DELETE /plants/{id}:** Delete a specific plant.
- GET /activities:** Retrieve a list of all activities.
- POST /activities:** Add a new activity.
- PUT /activities/{id}:** Update a specific activity.
- DELETE /activities/{id}:** Delete a specific activity.

## Contributing
To contribute:
1. Fork the repository.
2. Create a new branch (`git checkout -b feature/your-feature`).
3. Commit your changes (`git commit -m 'Add some feature'`).
4. Push to the branch (`git push origin feature/your-feature`).
5. Open a pull request.

## License
This project is licensed under the MIT License. See the `LICENSE` file for details.

