
# HuevoPadigal Project

Welcome to the HuevoPadigal project! This project aims to manage and analyze egg sales data for a business. It is divided into several subprojects for better organization and management.

## Project Structure

1. **API**: 
   - **Description**: The API is built using ASP.NET Core and Entity Framework Core. It manages the data for the egg sales, including CRUD operations for sales data.
   - **Location**: This will be in its own branch within the `HuevoPadigal` repository.

2. **Frontend** (Future Work):
   - **Description**: The frontend will provide a user interface for interacting with the API. This may include features for viewing and managing sales data.
   - **Location**: To be created in a future branch.

3. **Database**:
   - **Description**: The database schema is managed using Entity Framework Migrations. It stores sales data and is connected to the API.
   - **Location**: Managed within the API branch.

## Getting Started

### Prerequisites

- .NET 6 SDK or later
- SQL Server or SQL Server Express
- Visual Studio or any other IDE that supports .NET development

### Setup

1. **Clone the Repository**
   ```bash
   git clone https://github.com/yourusername/HuevoPadigal.git
   ```

2. **Navigate to the Project Directory**
   ```bash
   cd HuevoPadigal
   ```

3. **Set Up the API**
   - Navigate to the API directory:
     ```bash
     cd PadigalAPI
     ```
   - Restore the NuGet packages:
     ```bash
     dotnet restore
     ```
   - Apply migrations to the database:
     ```bash
     dotnet ef database update
     ```
   - Run the API:
     ```bash
     dotnet run
     ```

4. **Access the API**
   - Open Postman or any API client.
   - Use the following base URL to access the API:
     ```
     http://localhost:5000/api
     ```

## Branching Strategy

- **main**: The main branch will contain the initial setup and documentation.
- **api**: This branch will contain the API project with its own commits and history.
- **frontend**: Future branches will be created for frontend development.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## Contact

For any inquiries, please contact me at [renataunda11@gmail.com](mailto:renataunda11@gmail.com)