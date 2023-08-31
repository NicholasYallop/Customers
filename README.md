# Functionality
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

# Reasoning
One project to host server, one project to read & send POST request  

Created test.csv and built functionality until all requirements met  

Built csv_Poster until intended POST request was correctly sent  
 -> console application; uses stdout for debug information  
 -> uses reader to read lines from .csv file  
 -> used Json serialisation to send & recieved customer enumerables as http body  

Built Server until POST request was correctly handled  
 -> ASP .NET API application  
 -> used SQLite to dummy a database, and used INSERT/REPLACE & SELECT statements to store/read data  
    -> database .db file defined in appsettings  
 -> used one controller for both endpoints, as both deal with the same business object  
