# Middle Tier API

# DataBase
This application uses User-Secrets to store the connection string with the database name "MiddleTierAPI".
Migration

# Migration
The Data project has the class "MiddleTierAPIDbContextFactory" that is used to execute Entity Framework Migrations,
to use it is necessary to configure the ConnectionString on the appsettings on the same project for SQL Server.
Alternatively, the SQL Script available at "MiddleTierAPI.sql" can be executed to create the database

# DataBase API
To connect to the Data Base API Aplication set the URL on the DB_API_URL property present on the appsettings.json

# JWT
JWT configuration are set on appsettings under the section AppSettings

# curl example
# Register
curl --location --request POST 'http://localhost:5000/api/v1.0/Register' --header 'Content-Type: application/json' -d '{"email": "test@api.ie", "password":"Api@2021", "confirmpassword":"Api@2021"}'

# Login
curl --location --request POST 'http://localhost:5000/api/v1.0/Login' --header 'Content-Type: application/json' -d '{"email": "admin@api.ie", "password":"Api@2021"}'

# GetAll
curl --location --request GET 'http://localhost:5000/api/v1.0/Companies/'

# GetByISIN
curl --location --request GET 'http://localhost:5000/api/v1.0/Companies/GetByISIN?ISIN=US4578235699' -H "Authorization: Bearer YOUR_TOKEN_HERE"

# GetById
curl --location --request GET 'http://localhost:5000/api/v1.0/Companies/GetById?Id=d2b36c62-d8db-4a82-a080-08d9927770eb' -H "Authorization: Bearer YOUR_TOKEN_HERE"

# Add
curl --location --request POST 'http://localhost:5000/api/v1.0/Companies/' --header 'Content-Type: application/json' -d '{"name":"", "Exchange":"", "Ticker":"", "ISIN":"", "WebSite":""}' -H "Authorization: Bearer YOUR_TOKEN_HERE"

# Update
curl --location --request PUT 'http://localhost:5000/api/v1.0/Companies?Id=5d33f242-740c-46af-0688-08d991654a69' --header 'Content-Type: application/json' -d '{"name":"", "Exchange":"", "Ticker":"", "ISIN":"", "WebSite":""}' -H "Authorization: Bearer YOUR_TOKEN_HERE"
