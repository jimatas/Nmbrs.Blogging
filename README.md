## Blogging API

This project implements a blogging API per the technical assessment, using C#, ASP.NET Core, Entity Framework Core, and SQLite.

It provides endpoints for creating and retrieving blog posts, the latter with the option to include the author’s information.

## Requirements

To build and run the application:

- .NET 10 SDK

To build and run the application as a container:

- Docker (e.g., Docker Desktop)

## Running the application

### Using .NET

From the repository root:

`dotnet run --project src/Blogging.Api`

The API will be available at:

http://localhost:8080

### Using Docker

Build the image from the repository root:

`docker build -f src/Blogging.Api/Dockerfile -t blogging-api src`

Next, run the container:

`docker run --rm -p 8080:8080 blogging-api`

The API will be available at:

http://localhost:8080

## Seed data

The SQLite database is automatically created and migrated when the application starts.

Since author management is outside the scope of this project (also see Assumptions), a single author is seeded for use when creating blog posts:

- ID: `01a062ac-36a4-7060-9b5d-331347ac7c3f`
- Name: Jim
- Surname: Bosatlas

## API usage

The API exposes the following endpoints:

- `POST /post` - creates a new blog post
- `GET /post/{id}` - retrieves a post
- `GET /post/{id}?include=author` - retrieves a post including the author’s information

Example requests can be found in:

`src/Blogging.Api/Blogging.Api.http`

The OpenAPI document is available at:

http://localhost:8080/openapi/v1.json

## Testing

The project contains a minimal but useful suite of unit and integration tests.

Run the test suite from the repository root:

`dotnet test`

To run it with code coverage:

`dotnet test --coverlet --coverlet-output-format cobertura`

Application line coverage is approximately 96%.

## Design considerations

The architecture has intentionally been kept simple for this project. Controllers access the `DbContext` directly instead of introducing application or service layers.

## Assumptions

For the scope and time investment of this technical assessment, the following assumptions were made:

- Author management is out of scope; a single author is seeded.
- Only JSON is currently supported, but the API is designed so that other formats can be introduced without changing its design.
