# Vessel Management API

This repository contains a sample implementation of a Vessel Management API built with ASP.NET Core minimal APIs. The API uses the CQRS pattern with MediatR, EF Core with an in-memory database, FluentValidation for request validation, and integrated logging and error handling. The solution also includes a separate test project using xUnit and Moq.

## Features

- **RESTful API** using ASP.NET Core minimal APIs
- **CQRS Pattern:** Commands and queries are separated using MediatR
- **Entity Framework Core:** In-memory database provider for simplicity
- **FluentValidation:** Shared validations for vessel create/update requests
- **Global Error Handling:** Uses ProblemDetails for standardized error responses
- **Logging:** Configured logging across handlers and middleware for troubleshooting
- **DTOs:** Uses Data Transfer Objects to expose only necessary data
- **Mapping Extensions:** Provides extension methods to map domain models to DTOs
- **Unit Tests:** Separate test project to verify command and query handlers

## Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Visual Studio Code](https://code.visualstudio.com/) or [Visual Studio](https://visualstudio.microsoft.com/)
- Git

### Installation

1. **Clone the repository:**

   ```bash
   git clone https://github.com/drobA07/vessel-management.git
   cd vessel-management

2. **Restore dependencies:**

   ```bash
   dotnet restore

### Running the API
   
   To run the API locally, execute the following command in the project directory:

   ```bash
   dotnet run --project .\src\VesselManagement.Api\VesselManagement.Api.csproj
   ```

   The API will run on https://localhost:7052 or http://localhost:5214. In development mode, Open API documentation (Swagger UI) is available.

### API Endpoints
    
    POST /api/vessels
    Register a new vessel.

    PUT /api/vessels/{id}
    Update an existing vessel.

    GET /api/vessels
    Retrieve a list of all vessels.

    GET /api/vessels/{id}
    Retrieve a specific vessel by ID.

### Request Model Example
The API accepts JSON payloads for vessel registration and updates. For example:

  ```json
  {
    "name": "Test Vessel",
    "imo": "IMO1234567",
    "type": "Cargo",
    "capacity": 5000
  }
  ```

### Running Tests
A separate test project is provided. To run the tests, execute:

  ```bash
  dotnet test
  ```

This command builds and runs all unit tests for the command and query handlers.
