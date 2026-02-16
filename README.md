# Car Rental Web API

ASP.NET Core Web API project for managing a car rental system.

## Features
- Users with role-based authorization (Admin / Customer)
- Cars with categories, features, and locations
- Rentals with availability checks
- Payments linked to rentals
- JWT authentication
- FluentValidation
- Clean layered architecture (Controllers / Services / Entities)

## Tech Stack
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- JWT Authentication
- FluentValidation
- Postman for testing

## Project Structure
- **Api** – Controllers, DTOs, Authentication
- **Common** – Entities, DbContext, Services, Validation

## How to Run
1. Update connection string in `appsettings.json`
2. Run database migrations
3. Start the API
4. Test endpoints using Postman

## Postman

A complete Postman collection is included in the `/postman` folder.

You can import it into Postman and test all endpoints.

## Demo
All endpoints are tested via Postman.