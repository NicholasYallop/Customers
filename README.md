csv_Poster:
Accepts command line arguments; --csv [0] --endpoint [1]
Where [0] is the qualified filepath of the csv to be read & [1] is the URI of the endpoint to which the POST request should be sent.

Server:
Runs with IIS on localhost; opens on port 7017 (if available)
Uses SQLite to dummy the SQL Server
Has a Customer controller; "/Customer" endpoint for both POST & GET

To run full behaviour:
Run Server.csproj
Run csv_Poster.csproj, targeting desired csv, with endpoint=https://localhost:7017/Customers

# Customers