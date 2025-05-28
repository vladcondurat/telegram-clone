# Chatly

A feature-rich backend implementation of a modern chat application, providing robust messaging and real-time communication capabilities. This project implements a modern, scalable architecture for handling chat rooms, direct messages, and real-time notifications.

![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![ASP.NET](https://img.shields.io/badge/ASP.NET-692172?style=for-the-badge&logo=dot-net&logoColor=white)
![SignalR](https://img.shields.io/badge/SignalR-1A1A1A?style=for-the-badge&logo=signalr&logoColor=red)
![Entity Framework](https://img.shields.io/badge/Entity%20Framework-232323?style=for-the-badge&logo=dot-net&logoColor=white)

## Features

### Authentication & User Management

- User registration and authentication
- Profile management
- User search and discovery
- Online/offline status tracking

### Messaging System

- Real-time direct messaging
- Group chat rooms
- Message history and persistence
- Message delivery status
- Typing indicators

### Room Management

- Create and manage chat rooms
- Add/remove participants
- Room settings and permissions
- Room search and discovery

### Real-time Communication

- Instant message delivery
- Real-time status updates
- Typing indicators
- Online presence tracking

## Technical Architecture

### Backend Services

The application is built using a microservices architecture with the following components:

1. **WebApi (Main API)**

   - RESTful API endpoints
   - Authentication and authorization
   - User management
   - Room management
   - Message handling

2. **WebApi.RealTime (Real-time Service)**
   - SignalR hub implementation
   - Real-time message broadcasting
   - Connection management
   - Presence tracking

### Data Layer

- **Entity Framework Core** for data access
- **Repository Pattern** implementation
- **Code-First** database design
- **Migrations** for database versioning

### Core Entities

- **User**: User profiles and authentication
- **Room**: Chat rooms and group conversations
- **Message**: Individual messages and their metadata
- **UserRoom**: Many-to-many relationship between users and rooms

### API Structure

The API is organized into several controllers:

- `AuthController`: Handles authentication and user registration
- `UserController`: Manages user profiles and settings
- `RoomController`: Handles chat room operations
- `MessageController`: Manages message sending and retrieval
- `ApplicationController`: General application endpoints

## Tech Stack Details

### Backend Framework

- **.NET Core**: Modern, cross-platform framework
- **ASP.NET Core**: Web API framework
- **Entity Framework Core**: ORM for database operations
- **SignalR**: Real-time communication framework

### Database

- **SQL Server**: Primary database
- **Entity Framework Core Migrations**: Database versioning
- **Repository Pattern**: Data access abstraction

### Security

- **JWT Authentication**: Secure API access
- **Identity Framework**: User management
- **Role-based Authorization**: Access control

### Real-time Features

- **SignalR Hubs**: Real-time communication
- **WebSocket Support**: Efficient bi-directional communication
- **Connection Management**: User presence tracking

## Project Structure

```
├── WebApi/                           # Main API project
│   ├── Controllers/                 # API endpoints
│   ├── Models/                     # Data models and DTOs
│   ├── Middleware/                 # Custom middleware
│   ├── Configurations/             # App configurations
│   ├── Mappers/                    # Object mapping
│   ├── Attributes/                 # Custom attributes
│   ├── Migrations/                 # Database migrations
│   ├── Properties/                 # Project properties
│   ├── Program.cs                  # Application entry point
│   ├── WebApi.csproj              # Project file
│   ├── WebApi.http                # HTTP client file
│   └── appsettings.Template.json  # Template configuration
│
├── WebApi.RealTime/                # Real-time service
│   ├── Hubs/                      # SignalR hubs
│   ├── Consumers/                 # Message consumers
│   ├── Middleware/                # Custom middleware
│   ├── Properties/                # Project properties
│   ├── Program.cs                 # Application entry point
│   ├── WebApi.RealTime.csproj     # Project file
│   ├── ChatlyRealTimeServer.http  # HTTP client file
│   ├── appsettings.json           # Configuration
│   └── appsettings.Development.json # Development config
│
├── Services/                      # Business logic layer
│   ├── Features/                  # Feature implementations
│   ├── Mappers/                   # Object mapping
│   ├── Exceptions/                # Custom exceptions
│   ├── Constants/                 # Application constants
│   └── Services.csproj           # Project file
│
├── Data/                         # Data access layer
│   ├── Entities/                 # Database entities
│   ├── Repositories/             # Data access
│   ├── Infrastructure/           # Database infrastructure
│   ├── Contracts/                # Repository interfaces
│   ├── Constants/                # Data constants
│   └── Data.csproj              # Project file
│
├── .gitignore                    # Git ignore rules
├── TelegramClone.sln             # Solution file
└── README.md                     # Project documentation
```

## Getting Started

To get a local copy up and running, follow these simple steps:

1. **Prerequisites**

   - .NET 7.0 SDK or later
   - SQL Server (LocalDB or full instance)
   - Visual Studio 2022 or VS Code

2. **Clone the repository:**

   ```bash
   git clone https://github.com/vladcondurat/chatly.git
   ```

3. **Navigate to the project directory:**

   ```bash
   cd chatly
   ```

4. **Restore dependencies:**

   ```bash
   dotnet restore
   ```

5. **Configure the database:**

   - Update connection strings in `appsettings.json`
   - Run migrations:

   ```bash
   dotnet ef database update --project Data
   ```

6. **Run the services:**

   ```bash
   # Terminal 1 - Main API
   dotnet run --project WebApi

   # Terminal 2 - Real-time Service
   dotnet run --project WebApi.RealTime
   ```

## API Documentation

The API provides the following main endpoints:

### Authentication

- `POST /api/auth/register` - User registration
- `POST /api/auth/login` - User login
- `POST /api/auth/refresh` - Refresh token

### Users

- `GET /api/users` - Get user list
- `GET /api/users/{id}` - Get user details
- `PUT /api/users/{id}` - Update user profile

### Rooms

- `GET /api/rooms` - Get room list
- `POST /api/rooms` - Create new room
- `GET /api/rooms/{id}` - Get room details
- `PUT /api/rooms/{id}` - Update room settings

### Messages

- `GET /api/messages/room/{roomId}` - Get room messages
- `POST /api/messages` - Send new message
- `PUT /api/messages/{id}` - Update message
- `DELETE /api/messages/{id}` - Delete message

## Contributing

Contributions are what make the open source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## License

This project is licensed under the MIT License - see the LICENSE file for details.
