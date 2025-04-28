# TalkNest

## **Overview**
TalkNest is a modern web application built with:

Backend: .NET 8 (ASP.NET Core Web API)

Database: PostgreSQL

Frontend: Angular (optional separate setup)

Testing: Integration tests using Testcontainers for PostgreSQL
This solution is a modern, scalable, and modular application designed with extensibility and maintainability in mind. 
Using **Onion Architecture**, **CQRS**, and **FluentValidation**, with tools and libraries like **Serilog**, **EasyCache**, **MediatR**, **Swagger**, and robust testing frameworks such as **xUnit** and **Moq**.

---

## **Key Features**

### **1. Logging with Serilog**
- **Current Implementation**:
  - Logs application events and errors to the console.
- **Future-Ready**:
  - Configurable to log to centralized observability tools such as **Logstash**, **Sentry**, or **Elastic Stack**.
- **Benefits**:
  - Enables clear and structured logging, facilitating easier debugging and monitoring.

---


### **2. Command and Query Handling with MediatR**
- **Implementation**:
  - Utilizes **MediatR** for managing commands, queries, and domain events in a decoupled manner.
- **Advantages**:
  - Simplifies request-response handling.
  - Promotes a clean and maintainable architecture by separating business logic from infrastructure concerns.

---

### **3. Onion Architecture**
- **Layered Design**:
  - Implements **Onion Architecture** for clear separation of concerns:
    - **Core Layer**: Contains domain entities, interfaces, and core logic.
    - **Application Layer**: Handles business rules, command/query processing, and validation.
    - **Infrastructure Layer**: Manages database contexts, repositories, and external dependencies.
    - **Presentation Layer**: Hosts API endpoints for client interactions.

---

### **4. Validation with FluentValidation**
- **Implementation**:
  - Integrated **FluentValidation** for request validation.
  - Added a custom **`RequestValidationBehavior`** preprocessor to the MediatR pipeline for centralized validation logic.
- **Advantages**:
  - Ensures consistent validation across the application while keeping the logic clean and reusable.

---

### **5. Database Management**
- **Separated Command and Query Repositories**:
  - Distinct repositories for **command** (write) and **query** (read) operations, adhering to CQRS principles.
  - `ApplicationReadDb`: Handles read queries.
  - `WriteDbContext`: Manages write operations.
- **PostgreSQL Database**:
  - PostgreSQL is used as the main database
  - The connection string is automatically configured via environment variables in Docker Compose.
  - During startup, TalkNest checks for pending EF Core migrations and applies them.
  - No manual database setup is needed!
---

### **6. API Versioning**
- **Implementation**:
  - Added **API versioning** to ensure backward compatibility and smooth future updates.
- **Reason for Versioning**:
  - Enables introducing breaking changes in a controlled manner while continuing to support older versions of the API.
  - Facilitates seamless evolution of the API without disrupting existing clients.
- **Versioning in Practice**:
  - Supports versioning through URL path segments (e.g., `/api/v1/resource`).

---

### **7. API Documentation with Swagger**
- **Implementation**:
  - Integrated **Swagger** to generate comprehensive API documentation.
---

### **8. Integration Testing with Testcontainers**
TalkNest uses Testcontainers for integration tests in C#.
Testcontainers automatically starts a real PostgreSQL container during test execution to simulate production behavior.
Real PostgreSQL behavior
No local installation dependency
Clean database for each test run
Each test starts a fresh, isolated PostgreSQL database inside a Docker container.
- - **xUnit**:
  - Used for writing unit tests.
- **Moq**:
  - Utilized for mocking dependencies in unit tests.
  - Ensures that the application logic is tested independently of external services or infrastructure.
---

### **9. Custom Error Handling with ProblemDetails**

The solution introduces custom exceptions to encapsulate specific application logic errors. `ProblemDetails` is used in the `AddCustomErrorHandlingMiddleware` to map custom exceptions to structured error responses with relevant HTTP status codes and details. It dynamically checks the environment to include or exclude exception details for better security in production. Unhandled exceptions are logged based on a customisable condition, ensuring clarity for debugging while avoiding unnecessary logs for known exception types like validation errors.

## **Technologies and Tools**

- **Logging**: Serilog
- **Caching**: EasyCache
- **Request Handling**: MediatR
- **Validation**: FluentValidation
- **Architecture**: Onion Architecture, CQRS
- **API Management**: API Versioning, Swagger
- **Database**:
  -Sqlite
      - ApplicationReadDb (for reads)
      - DOCOsoftWriteDbContext (for writes)
- **Testing**: xUnit, Moq

---
### **Handling Forbidden Words**

The **Forbidden Words** feature is encapsulated in a dedicated singleton service, `ForbidWords`, to centralize the logic for validating and managing restricted terms. It is implemented as a **lazy-loaded** to ensure the forbidden words list is fetched only once (e.g., from a database or configuration) and reused across the application, enhancing performance and consistency. Future updates may include loading from an external source, ensuring flexibility and scalability without altering core logic.
## **How to Run the Solution**

1. **Prerequisites**:
   - Install [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0).
   - Install [Docker](https://www.docker.com/).
   - Install [Docker Compose](https://docs.docker.com/compose/).
   - Verify installations:
   - dotnet --version
   - docker --version
   - docker compose version
2. Build Docker image for API	
- cd talknest
- docker build -t talknest .
- docker compose up --build
This will build (if needed) and start all services.
- http://127.0.0.1:8000/swagger/index.html

3.Host API + PostgreSQL using Docker Compose	

3. **Configuration**:
   - It is possible to modify `appsettings.json` to change logging, caching, API versioning if needed.
   Before starting, make sure you have installed:

.NET 8 SDK

Docker

Docker Compose

---

For any questions or issues, feel free to contact me.