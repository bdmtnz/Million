# 🏡 Property Management API

A robust and scalable backend API built with ASP.NET Core to manage property information. This project follows a clean architecture approach and uses a CQRS pattern to separate command and query responsibilities.

## 📝 Table of Contents

  - [Key Features](https://www.google.com/search?q=%23-key-features)
  - [Technologies Used](https://www.google.com/search?q=%23-technologies-used)
  - [Getting Started](https://www.google.com/search?q=%23-getting-started)
  - [Running Tests](https://www.google.com/search?q=%23-running-tests)
  - [API Documentation](https://www.google.com/search?q=%23-api-documentation)
  - [Error Handling](https://www.google.com/search?q=%23-error-handling)

-----

## ✨ Key Features

  - **Property CRUD:** Operations to create, read, and manage properties.
  - **Advanced Filtering and Search:** Allows searching for properties by keyword (name, address, year) in a case-insensitive and space-agnostic manner.
  - **Pagination and Price Range:** Supports paginating results and filtering by a specific price range.
  - **Database Abstraction:** Uses a generic repository pattern to decouple business logic from the MongoDB implementation.
  - **CQRS Architecture:** Implements MediatR for a clear data flow and high cohesion in the business logic.

-----

## 💻 Technologies Used

  - **Backend:**
      - **C\# / ASP.NET Core:** Primary framework.
      - **MongoDB:** NoSQL database.
      - **MongoDB.Driver:** Driver for interacting with MongoDB.
      - **MediatR:** CQRS implementation for handling commands and queries.
      - **Mapster:** For object DTO mapping (`Property` -\> `PropertyResponse`).
  - **Testing:**
      - **NUnit:** Unit testing framework.
      - **Moq:** For creating mock objects of dependencies.
      - **FluentAssertions:** For more readable assertions.

-----

## ▶️ Getting Started

Follow these steps to set up and run the application in your local environment.

### **1. Prerequisites**

  - **.NET 8.0 SDK** (or higher).
  - **Docker** (recommended for running a local MongoDB instance).

### **2. Configuration**

  - Clone the repository:
    ```bash
    git clone https://github.com/bdmtnz/Million.git
    cd Million/Million.BackEnd/Million.BackEnd.Api
    ```
  - Create a MongoDB container (if you don't have one running):
    ```bash
    docker run --name my-mongo -p 27017:27017 -d mongo
    ```
  - Ensure your connection string in `appsettings.json` points to your MongoDB instance. By default, it should be:
    ```json
    "PersistenceSettings": {
      "ConnectionString": "mongodb://localhost:27017",
      "Collection": "properties_db"
    }
    ```

### **3. Installation and Running**

  - Restore NuGet packages:
    ```bash
    dotnet restore
    ```
  - Run the API project:
    ```bash
    dotnet run
    ```

The API will be available at `http://localhost:5122` (or the port configured in `launchSettings.json`).

  - Open Swagger Documentation:
    `http://localhost:5122/swagger/index.html`

  - Create seeder:
    `Execute the create seeder endpoint /api/Seeder`
-----

## ✅ Running Tests

To ensure code quality and correctness, you can run all unit tests with the following command:

```bash
dotnet test
```

-----

## 📄 API Documentation

The following endpoints are available to consume the API:

  - **`GET /api/Property`**

      - **Description:** Retrieves a list of properties with filtering, pagination, and sorting options.
      - **Query Parameters:**
          - `keyword` (optional): Filters by name, address, or year.
          - `limit` (optional): Number of results per page.
          - `offset` (optional): Page to retrieve.
          - `from` (optional): Minimum price.
          - `to` (optional): Maximum price.

  - **`GET /api/Property/{id}`**

      - **Description:** Retrieves a specific property by its ID.
      - **Route Parameters:**
          - `id` (required): The unique property ID.

-----

## 🚫 Error Handling

The project uses the **`ErrorOr`** library for robust error handling. When an operation fails, the API returns a `ProblemDetails` with an appropriate HTTP status code (`404 Not Found`, `500 Internal Server Error`, etc.), providing clear information about the issue.