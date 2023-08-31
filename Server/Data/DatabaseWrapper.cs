using ESG_CSVreader;
using Microsoft.Data.Sqlite;
using System.Diagnostics.Metrics;

namespace ESG_customerAPI.Data
{
    public class DatabaseWrapper
    {
        public static string data_dir { get; set; }

        private string ConnectionString => "Data Source=" + data_dir;

        public DatabaseWrapper()
        {
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();

                command.CommandText = @"
                    CREATE TABLE IF NOT EXISTS customers (
	                    Customer_Ref TEXT PRIMARY KEY,
                        Customer_Name TEXT,
                        Address_Line_1 TEXT,
                        Address_Line_2 TEXT,
                        Town TEXT,
                        County TEXT,
                        Country TEXT,
                        Postcode TEXT
                    );                    
                ";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read()) { }
                }
            }
        }

        public void Save(IEnumerable<Customer> customers)
        {
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();

                foreach (var customer in customers)
                {
                    var command = connection.CreateCommand();

                    command.CommandText = @"
                    INSERT OR REPLACE INTO customers (
                        Customer_Ref,
                        Customer_Name,
                        Address_Line_1,
                        Address_Line_2,
                        Town,
                        County,
                        Country,
                        Postcode)
                    VALUES (
                        @Customer_Ref,
                        @Customer_Name,
                        @Address_Line_1,
                        @Address_Line_2,
                        @Town,
                        @County,
                        @Country,
                        @Postcode)";

                    command.Parameters.AddWithValue("@Customer_Ref", customer.Customer_Ref);
                    command.Parameters.AddWithValue("@Customer_Name", customer.Customer_Name);
                    command.Parameters.AddWithValue("@Address_Line_1", customer.Address_Line_1);
                    command.Parameters.AddWithValue("@Address_Line_2", customer.Address_Line_2);
                    command.Parameters.AddWithValue("@Town", customer.Town);
                    command.Parameters.AddWithValue("@County", customer.County);
                    command.Parameters.AddWithValue("@Country", customer.Country);
                    command.Parameters.AddWithValue("@Postcode", customer.Postcode);

                    using (var reader = command.ExecuteReader()) { }
                }
            }
        } 

        public IEnumerable<Customer> Read(string reference)
        {
            var customers = new List<Customer>();

            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();

                command.CommandText = @"SELECT * FROM customers WHERE Customer_Ref=@reference";

                command.Parameters.AddWithValue("@reference", reference);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        customers.Add(new Customer()
                        {
                            Customer_Ref = reader.GetString(0),
                            Customer_Name = reader.GetString(1),
                            Address_Line_1 = reader.GetString(2),
                            Address_Line_2 =  reader.GetString(3),
                            Town = reader.GetString(4),
                            County = reader.GetString(5),
                            Country = reader.GetString(6),
                            Postcode = reader.GetString(7)
                    });
                    }
                }
            }

            return customers;
        }
    }
}
