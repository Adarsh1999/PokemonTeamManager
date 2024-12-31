# Pokemon Team Manager

A .NET application for managing Pokémon teams. This project provides a RESTful API with Swagger documentation and includes database support using PostgreSQL. The application is designed to be easy to set up and extend, making it ideal for learning or managing your Pokémon team data.

## Features
- RESTful API for managing Pokémon teams and their members.
- PostgreSQL integration for persistent data storage.
- Swagger UI for API documentation and testing.
- Docker Compose for easy setup and deployment.

---

## Prerequisites

Before you begin, ensure you have the following installed:
- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/)
- [Docker Compose](https://docs.docker.com/compose/)

---

## Setup

### 1. Clone the Repository
```bash
git clone <repository-url>
cd <repository-directory>
```

### 2. Configure PostgreSQL with Docker Compose
This project includes a `docker-compose.yml` file to simplify the PostgreSQL setup.

#### Start the PostgreSQL container:
```bash
docker-compose up -d
```

This will start a PostgreSQL container with the following default configuration:
- Host: `localhost`
- Port: `5432`
- Database: `pokemondb`
- Username: `postgres`
- Password: `postgres`

If needed, you can update these values in the `docker-compose.yml` file.

#### Check the PostgreSQL container status:
```bash
docker ps
```

### 3. Update the Database Connection String
Ensure the connection string in `appsettings.json` matches the PostgreSQL configuration:
```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Database=pokemondb;Username=postgres;Password=postgres"
}
```

### 4. Restore .NET Dependencies
Run the following command to restore all required .NET dependencies:
```bash
dotnet restore
```
### 5. If no migrations folder exists, create one
Run the following command to create a migrations folder:
```bash
dotnet ef migrations add InitialCreate
```
### 6. Apply Database Migrations
To set up the database schema, apply the Entity Framework Core migrations:
```bash
dotnet ef database update
```

---

## Running the Application

### 1. Start the Application
Run the following command to start the application:
```bash
dotnet run
```

### 2. Access the Swagger Documentation
Once the application is running, open your browser and navigate to the Swagger UI:
- **HTTP**: [http://localhost:5077/swagger](http://localhost:5077/swagger)
- **HTTPS**: [https://localhost:7073/swagger](https://localhost:7073/swagger)

---

## Additional Commands

### Build the Project
Compile the project using:
```bash
dotnet build
```

### Run Tests
Execute the test suite with:
```bash
dotnet test
```

---

## Project Structure
- **`Program.cs`**: Entry point of the application.
- **`appsettings.json`**: Configuration file for application settings.
- **`Properties/launchSettings.json`**: Configuration file for launch settings.

---

## Docker Compose File (Sample)
Ensure your `docker-compose.yml` file is properly configured as follows:
```yaml
version: '3.8'
services:
  postgres:
    image: postgres:15
    container_name: pokemon_postgres
    environment:
      POSTGRES_USER: postgres
      POSTGRES_PASSWORD: postgres
      POSTGRES_DB: pokemondb
    ports:
      - "5432:5432"
    volumes:
      - postgres_data:/var/lib/postgresql/data

volumes:
  postgres_data:
```

---

## Notes
- Ensure that Docker is running before starting the PostgreSQL container.
- The application supports additional database operations and extensions via Entity Framework Core.

Feel free to extend the application or modify the setup to suit your needs!
