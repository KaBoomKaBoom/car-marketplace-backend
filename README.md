# Car Marketplace Backend API Documentation

[![Ask DeepWiki](https://deepwiki.com/badge.svg)](https://deepwiki.com/KaBoomKaBoom/car-marketplace-backend)

## Overview

The Car Marketplace Backend is a RESTful API built with .NET 9.0 and PostgreSQL, designed to manage a car marketplace application. It provides authentication, user management, and car listing functionality with role-based access control.

## Technology Stack

- **Framework**: .NET 9.0 (ASP.NET Core Web API)
- **Database**: PostgreSQL
- **Authentication**: JWT (JSON Web Tokens)
- **ORM**: Entity Framework Core
- **Containerization**: Docker & Docker Compose
- **Password Hashing**: BCrypt.NET
- **Documentation**: Swagger/OpenAPI

## Architecture

The application follows a layered architecture with:
- **Controllers**: Handle HTTP requests and responses
- **Models**: Define data entities
- **DTOs**: Data Transfer Objects for API communication
- **Data Context**: Entity Framework database context
- **Helpers**: Utility classes (JWT, Database Seeder)

## Getting Started

### Prerequisites

- Docker and Docker Compose
- .NET 9.0 SDK (for development)

### Running with Docker Compose

1. Clone the repository
2. Navigate to the project root directory
3. Run the application:

```bash
docker-compose up --build
```

The API will be available at:
- **API Base URL**: `http://localhost:8080`
- **Swagger Documentation**: `http://localhost:8080/swagger`
- **PostgreSQL Database**: `localhost:5432`

### Environment Configuration

#### Database Configuration
- **Host**: db (container) / localhost (local)
- **Port**: 5432
- **Database**: db
- **Username**: admin
- **Password**: admin

#### Application Configuration
- **Port**: 8080
- **Environment**: Development (with CORS enabled for localhost)

## Authentication

The API uses JWT-based authentication with role-based authorization. Two roles are supported:
- **USER**: Regular users who can manage their own car listings
- **ADMIN**: Administrators with full access to all resources

### JWT Configuration
- **Secret Key**: `your_secret_keygjhzklS:KkcvfhadlKSEWRQ8OiasjvhbjcsmklX;oih`
- **Token Lifetime**: Validated
- **Claims**: User ID, Username, Role

## API Endpoints

### Authentication Endpoints (`/api/Auth`)

#### POST `/api/Auth/signup`
Register a new user account.

**Request Body:**
```json
{
  "username": "string",
  "email": "string",
  "password": "string",
  "role": "USER" // Optional, defaults to "USER"
}
```

**Response:**
- **201 Created**: User successfully created
- **400 Bad Request**: Invalid data or email already exists

#### POST `/api/Auth/login`
Authenticate user and receive JWT token.

**Request Body:**
```json
{
  "email": "string",
  "password": "string"
}
```

**Response:**
```json
{
  "token": "jwt_token_string"
}
```

**Status Codes:**
- **200 OK**: Authentication successful
- **401 Unauthorized**: Invalid credentials

#### POST `/api/Auth/token`
Generate a dummy token for testing purposes.

**Request Body:**
```json
{
  "role": "USER" // or "ADMIN"
}
```

### User Endpoints (`/api/User`)

All user endpoints require authentication with `USER` role.

#### GET `/api/User/cars`
Retrieve all car listings.

**Headers:**
```
Authorization: Bearer {jwt_token}
```

**Response:**
- **200 OK**: Array of car objects
- **404 Not Found**: No cars found

#### GET `/api/User/cars/{id}`
Retrieve a specific car by ID.

**Parameters:**
- `id` (path): Car ID

**Response:**
- **200 OK**: Car object
- **404 Not Found**: Car not found

#### POST `/api/User/addCar`
Add a new car listing.

**Request Body:**
```json
{
  "userId": 1,
  "make": "string",
  "model": "string",
  "year": 2024,
  "price": 25000.00,
  "mileage": 15000,
  "type": "string",
  "condition": "string",
  "category": "string",
  "fuel": "string",
  "imageUrl": "string",
  "horsePower": 200,
  "torque": 300.5,
  "transmission": "string",
  "color": "string",
  "interior": "string",
  "drive": "string",
  "description": "string",
  "cylinderCapacity": 2.0
}
```

**Response:**
- **201 Created**: Car successfully added
- **400 Bad Request**: Invalid model state

#### PUT `/api/User/updateCar/{id}`
Update an existing car listing (owner only).

**Parameters:**
- `id` (path): Car ID

**Request Body:** Same as addCar endpoint

**Response:**
- **200 OK**: Updated car object
- **403 Forbidden**: Not the car owner
- **404 Not Found**: Car not found

#### DELETE `/api/User/deleteCar/{id}`
Delete a car listing (owner only).

**Parameters:**
- `id` (path): Car ID

**Response:**
- **204 No Content**: Car successfully deleted
- **403 Forbidden**: Not the car owner
- **404 Not Found**: Car not found

#### GET `/api/User/profile`
Get current user's profile information.

**Response:**
```json
{
  "id": 1,
  "username": "string",
  "email": "string"
}
```

#### GET `/api/User/profile/cars`
Get current user's car listings.

**Response:**
- **200 OK**: Array of user's cars
- **404 Not Found**: No cars found for user

### Admin Endpoints (`/api/Admin`)

All admin endpoints require authentication with `ADMIN` role.

#### User Management

##### GET `/api/Admin/users`
Retrieve all users.

##### GET `/api/Admin/users/{id}`
Retrieve a specific user by ID.

##### POST `/api/Admin/addUser`
Add a new user.

**Request Body:**
```json
{
  "username": "string",
  "email": "string",
  "passwordHash": "string",
  "phoneNumber": "string",
  "role": "USER" // or "ADMIN"
}
```

##### PUT `/api/Admin/updateUser/{id}`
Update user information.

##### DELETE `/api/Admin/deleteUser/{id}`
Delete a user.

#### Car Management

##### GET `/api/Admin/cars`
Retrieve all cars.

##### GET `/api/Admin/cars/{id}`
Retrieve a specific car by ID.

##### POST `/api/Admin/addCar`
Add a new car (admin).

##### PUT `/api/Admin/updateCar/{id}`
Update any car listing.

##### DELETE `/api/Admin/deleteCar/{id}`
Delete any car listing.

## Data Models

### User Model
```json
{
  "id": "integer",
  "username": "string",
  "email": "string",
  "passwordHash": "string",
  "phoneNumber": "string",
  "role": "string" // "USER" or "ADMIN"
}
```

### Car Model
```json
{
  "id": "integer",
  "userId": "integer",
  "make": "string",
  "model": "string",
  "year": "integer",
  "price": "decimal",
  "mileage": "integer",
  "type": "string",
  "condition": "string",
  "category": "string",
  "fuel": "string",
  "imageUrl": "string",
  "horsePower": "integer",
  "torque": "decimal",
  "transmission": "string",
  "color": "string",
  "interior": "string",
  "drive": "string",
  "description": "string",
  "cylinderCapacity": "decimal",
  "createdAt": "datetime"
}
```

## Error Handling

The API returns consistent error responses with appropriate HTTP status codes:

- **400 Bad Request**: Invalid request data
- **401 Unauthorized**: Authentication required
- **403 Forbidden**: Insufficient permissions
- **404 Not Found**: Resource not found
- **500 Internal Server Error**: Database or server errors

Example error response:
```json
{
  "error": "Error message description"
}
```

## CORS Configuration

The API is configured with different CORS policies:

**Development:**
- Allowed Origins: `http://localhost:32768`, `http://localhost:3000`, `http://localhost:8000`
- Allows all methods and headers
- Supports credentials

**Production:**
- Allowed Origins: `https://myProductionSite.com`
- Allows all methods and headers
- Supports credentials

## Database

### PostgreSQL Configuration
- **Image**: postgres (latest)
- **Container**: postgres_db_web
- **Data Persistence**: `./data` volume mounted
- **Port**: 5432

### Database Seeding
The application includes a database seeder that populates initial data from a `cars.json` file during startup.

## Security Features

1. **Password Hashing**: Uses BCrypt for secure password storage
2. **JWT Authentication**: Stateless authentication with configurable expiration
3. **Role-Based Authorization**: Separate permissions for users and admins
4. **Owner-Based Access Control**: Users can only modify their own car listings
5. **Input Validation**: Model validation for all API endpoints

## Development

### Building the Application
```bash
dotnet build
```

### Running Locally
```bash
dotnet run
```

### Testing with Swagger
Visit `http://localhost:8080/swagger` to access the interactive API documentation and testing interface.

## Deployment

The application is containerized and ready for deployment using Docker. The provided Docker Compose configuration sets up both the API and PostgreSQL database with proper networking and environment variables.

### Production Considerations
1. Update JWT secret key and database credentials
2. Configure production CORS origins
3. Enable HTTPS redirection
4. Set up proper logging and monitoring
5. Configure database backups
6. Use environment-specific configuration files

## Support

For issues and questions, refer to the Swagger documentation at `/swagger` endpoint or check the application logs for detailed error information.
