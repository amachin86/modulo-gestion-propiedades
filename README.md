# Property Management API

## Overview

This project is a backend API for managing real estate properties, built with ASP.NET Core 8 using Clean Architecture principles. It implements Domain-Driven Design (DDD), CQRS, Repository and Unit of Work patterns, and follows SOLID principles for maintainable and scalable code.

## Features

- RESTful API for managing properties, hosts, users, bookings, and domain events.
- Endpoints for CRUD operations on properties with filtering and pagination.
- Synchronization endpoint for OTAs (e.g., Airbnb, Booking) that logs domain events.
- JWT-based authentication with token expiration.
- Entity Framework Core 8 with migrations for Microsoft SQL Server.
- FluentValidation for input validation.
- Centralized exception handling and structured logging with Serilog.
- API documentation with Swagger/OpenAPI.
- Dependency Injection for all services and repositories.

## Architecture

The solution is divided into four main projects:

- **Domain**: Contains entities, value objects, domain events, and repository interfaces.
- **Application**: Implements CQRS handlers, DTOs, validation, and application services.
- **Infrastructure**: Contains EF Core DbContext, repository implementations, Unit of Work, and migrations.
- **WebAPI**: ASP.NET Core Web API project with controllers, middleware, authentication, and configuration.

## Getting Started

### Prerequisites

- .NET 8 SDK
- Microsoft SQL Server
- (Optional) Postman or similar tool for API testing

### Setup

1. Clone the repository.
2. Update the connection string in `appsettings.json` in the WebAPI project.
3. Run database migrations:
   Move to PropertyManagement.Infrastructure
   ```
   dotnet ef database update 
   ```
4. Build and run the WebAPI project:
   Move to /PropertyManagement.WebAPI
   ```
   dotnet run 
   ```
   or 
    ```
   dotnet watch run
   ```
5. Access Swagger UI at `http://localhost:5074/index.html` to explore and test the API.

## API Endpoints

### Authentication
- `POST /api/auth/login` - Login with credentials and get JWT token.
- `POST /api/auth/register` - Register a new user.

### Properties
- `GET /api/properties` - List properties with optional filters (name, hostId, status) and pagination.
- `GET /api/properties/{id}` - Get property details by ID.
- `POST /api/properties` - Create a new property.
- `PUT /api/properties/{id}` - Update an existing property.
- `DELETE /api/properties/{id}` - Delete a property.

### Hosts
- `GET /api/hosts` - List hosts with optional filters (name, email) and pagination.
- `GET /api/hosts/{id}` - Get host details by ID.
- `POST /api/hosts` - Create a new host.
- `PUT /api/hosts/{id}` - Update an existing host.
- `DELETE /api/hosts/{id}` - Delete a host.

### Bookings
- `GET /api/bookings` - List bookings with optional filters (propertyId, checkIn, checkOut) and pagination.
- `GET /api/bookings/{id}` - Get booking details by ID.
- `POST /api/bookings` - Create a new booking.
- `PUT /api/bookings/{id}` - Update an existing booking.
- `DELETE /api/bookings/{id}` - Delete a booking.

### Users
- `GET /api/users` - List users with optional filters (username, email, role) and pagination.
- `GET /api/users/{id}` - Get user details by ID.

### Domain Events
- `GET /api/domainevents` - List domain events with optional filters (propertyId, eventType, occurredAt) and pagination.
- `GET /api/domainevents/{id}` - Get domain event details by ID.
- `POST /api/domainevents` - Create a new domain event.
- `DELETE /api/domainevents/{id}` - Delete a domain event.

### Synchronization
- `POST /api/sync/ota` - Synchronize property with OTAs (logs domain events).

## Testing

- Use Swagger UI or API clients to test endpoints.
- Ensure JWT token is included in Authorization header for protected endpoints.
- Validate input data with FluentValidation rules.

## Contributing

Contributions are welcome. Please fork the repository and submit pull requests.

## License

This project is licensed under the MIT License.

## References

- [How to Write a Good README File](https://www.freecodecamp.org/news/how-to-write-a-good-readme-file/)
- [Clean Architecture](https://8thlight.com/blog/uncle-bob/2012/08/13/the-clean-architecture.html)
- [CQRS Pattern](https://docs.microsoft.com/en-us/azure/architecture/patterns/cqrs)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
- [FluentValidation](https://fluentvalidation.net/)
- [Serilog](https://serilog.net/)
- [Swagger/OpenAPI](https://swagger.io/specification/)
