# Restaurants API - Clean Architecture & Testing Playground

This is a **.NET 8.0 Web API** project designed for **managing restaurants** while following the principles of **Clean Architecture**.  
The project demonstrates advanced software engineering practices including **CQRS**, **MediatR**, **Identity & JWT Authentication**, **Logging**, **Testing**, and more.

---

## **What’s Implemented in This Project**

### 1. **Clean Architecture**
- Structured the solution into separate layers (**API**, **Application**, **Domain**, **Infrastructure**).
- Enforced separation of concerns and testability.

### 2. **Entity Framework Core Integration**
- Code-First approach with migrations.
- Database interaction through **EF Core** and **Repository patterns**.

### 3. **CQRS with MediatR**
- Implemented **CQRS** (Command and Query Responsibility Segregation) using **MediatR**.
- Separation of read and write operations for better scalability.

### 4. **Authentication & Authorization**
- Integrated **ASP.NET Core Identity** for secure user management.
- Implemented **JWT (JSON Web Token)** for authentication.
- Role-based authorization for securing endpoints.

### 5. **Validation**
- Used **FluentValidation** for request model validation.
- Added comprehensive test coverage for valid and invalid scenarios.

### 6. **AutoMapper**
- Simplified object-to-object mapping between DTOs and entities.

### 7. **Logging**
- Implemented structured logging using **Serilog** for better monitoring and debugging.

### 8. **Pagination**
- Added pagination support for retrieving restaurants efficiently.

### 9. **Testing**
- **xUnit** for unit testing.
- **Moq** for mocking dependencies.
- **FluentAssertions** for cleaner and more readable test assertions.
- **Integration Testing** for end-to-end API testing.

---

## **Technologies Used**
- **.NET 8.0 Web API**
- **Clean Architecture**
- **Entity Framework Core**
- **AutoMapper**
- **FluentValidation**
- **CQRS** + **MediatR**
- **ASP.NET Core Identity**
- **JWT Authentication**
- **Serilog Logging**
- **Pagination**
- **xUnit**, **Moq**, **FluentAssertions**
- **Integration Testing**
- **SQL Server**

---

## **Project Structure**

```plaintext
src/
│── Restaurants.API            → The main API project (entry point)
│── Restaurants.Application    → Application logic, CQRS, commands, queries, and validations
│── Restaurants.Domain        → Core domain models, entities, and business rules
│── Restaurants.Infrastructure → Database, repositories, identity, logging, and other infrastructure code
tests/
│── Restaurants.API.Tests      → Unit and integration tests


## **How To Run**

To run the project, set Restaurants.API as the startup project in Visual Studio and start the application.
