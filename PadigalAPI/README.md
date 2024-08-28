# PadigalAPI

This is the backend API for the Padigal project, a system designed to manage the sales and inventory of egg products. The API provides endpoints to manage clients, sales, and various related operations.

## Features

- **Client Management**: Create, update, delete, and retrieve client information.
- **Sales Management**: Create, update, delete, and retrieve sales records, including details like product types, quantities, prices, payment status, and more.
- **Custom Converters**: Supports custom JSON converters for handling enumerations such as `ClientType`, `ProductType`, `Frequency`, and `PaymentType`.
- **Exception Handling**: Global error handling is implemented to ensure meaningful error responses.
- **Logging**: Integrated logging for easier debugging and monitoring.
- **Swagger Documentation**: Auto-generated interactive API documentation with Swagger.
- **AutoMapper Integration**: Simplified mapping between entities and DTOs.

## Technologies Used

- **.NET Core**: The core framework used for building this API.
- **Entity Framework Core**: Used for database management.
- **Microsoft SQL Server**: The database system where the application data is stored.
- **AutoMapper**: For mapping between data models and DTOs.
- **Newtonsoft.Json**: Used for JSON serialization and deserialization with custom converters.
- **Swagger**: For API documentation.
- **Logging**: Console and debug logging via the built-in logging providers in .NET Core.

## Project Structure

- **Controllers**: Handle incoming HTTP requests and send responses back to the client.
- **Services**: Business logic layer that orchestrates operations between the controllers and repositories.
- **Repositories**: Data access layer that interacts with the database.
- **Models**: Define the data structures used across the API.
- **DTOs**: Data Transfer Objects used to transfer data between layers.
- **Converters**: Custom JSON converters for specific enums.
- **Mappers**: Configurations for AutoMapper.
- **Data**: Contains the database context and initial seed data.

## Setup Instructions

### Prerequisites

- .NET Core SDK
- Microsoft SQL Server

### Installation

1. **Clone the repository**:
   ```bash
   git clone https://github.com/renataunda/HuevoPadigal.git
   cd HuevoPadigal/PadigalAPI
   ```

2. **Set up the database**:
   - Update the connection string in `appsettings.json` with your SQL Server details.
   - Run the following commands to create the database and apply migrations:
     ```bash
     dotnet ef database update
     ```

3. **Run the API**:
   ```bash
   dotnet run
   ```

4. **Access Swagger**:
   - Navigate to `http://localhost:<port>/swagger` to view and interact with the API documentation.

## Usage

### Client Management

- **Get all clients**: `GET /api/clients`
- **Get client by ID**: `GET /api/clients/{id}`
- **Create a client**: `POST /api/clients`
- **Update a client**: `PUT /api/clients/{id}`
- **Delete a client**: `DELETE /api/clients/{id}`

### Sales Management

- **Get all sales**: `GET /api/sales`
- **Get sale by ID**: `GET /api/sales/{id}`
- **Create a sale**: `POST /api/sales`
- **Update a sale**: `PUT /api/sales/{id}`
- **Delete a sale**: `DELETE /api/sales/{id}`
- **Update payment details**: `PATCH /api/sales/{id}/payment`

## Error Handling

- All errors are logged and returned with a standardized JSON response.
- Global exception handling ensures that all unhandled exceptions are caught and appropriately logged.

## Logging

- Logs are written to the console and debug output.
- Logs include error messages and stack traces to assist with debugging.

## Future Enhancements

- Implement additional features and enhancements based on user feedback.
- Further optimizations and performance improvements.

## Contribution

This project is not open for public contributions at the moment. Any changes or enhancements will be managed internally.