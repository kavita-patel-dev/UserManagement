# User Management API

## Description
User Management API is RESTful web service built with .NET Core. This API has currently one endpoint to retrieve list of users with support of pagination and designed to extend and implement new functionality.

## Technologies Used
- .NET 8
- ASP.NET Core Web API
- Entity Framework Core (or other ORM if applicable)
- PostgreSQL and MySQL

## Features
- Retrieve user list with support of pagination

## Getting Started

### Prerequisites
- .NET 8
- Docker

### Installation
1. Clone the repository:
    ```bash
    https://github.com/kavita-patel-dev/UserManagement.git
    ```

2. Navigate to the project directory:
    ```bash
    cd UserManagement
    ```

3. Build Required Containers:
    ```bash
    docker-compose up --build -d
    ```

4. Verify list of Containers  
    ```bash
    docker ps
    ```
    ![image](https://github.com/user-attachments/assets/fc39832c-d2be-44c2-b665-6f6915f25fe0)

5. Verify default db (set to PostgreSQL)
    ```bash
    docker exec -it <container_id> printenv DB_PROVIDER
    ```
    ![image](https://github.com/user-attachments/assets/e8cc3694-1a3c-4876-8680-8688439147a4)

The API will be running at `https://localhost:8080`

## API Documentation

### Endpoint 1: `GET /api/Users`
- Description: Fetches the list of users based on skip and take paramters for pagination.
- Response: 200 OK with the list of users and totalUsers.

## Testing
- Using Postman you can test endpoint `http://localhost:8080/Users` with Get request
  ![image](https://github.com/user-attachments/assets/8bb52535-fc6d-46dc-b9ad-2accd61b367c)

- Using swagger on browser `http://localhost:8080/swagger/index.html`
  ![image](https://github.com/user-attachments/assets/7d9b8b8a-1fec-4a29-912c-71c3658a6b9c)
  ![image](https://github.com/user-attachments/assets/5b60f2ab-73c8-4cb7-8e05-a29999452911)
    
## Configuration
- This Project has docker-compose.yml file which is by default set as DB_PROVIDER=PostgreSQL under environment. When you build containers initially it will set to PostgreSQL database. 
- To switch to MySQL database execute below steps:
    1. docker stop <container_id> (docker ps will give you list of container running and provide api container Id here)
    2. docker rm <container_id>
    3. docker run -e DB_PROVIDER=MySQL -p 8080:8080 usermanagement-api
    4. verify MySQL is set as DB_PROVIDER with command docker exec -it <container_id> printenv DB_PROVIDER

## Notes
- When you build containers both MySQL and PostgreSQL databases will be created with required table and data seeder values. 
- There two different files created under UserManagement.Infrastructure -> DBScript -> Init_MySQL.sql and Init.PostgreSQL.sql which is used once only while building container and docker-compose.yml file has that path under volumes
- If you want to run this solution in VS 2022 then need to change connectionstring to have server name as localhost. Currently connectionstring has service name defined in docker-compose.yml file which is being used to communicate two containers(API,Database). 
