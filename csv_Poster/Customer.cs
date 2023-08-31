using System.Text.Json.Serialization;

namespace ESG_CSVreader
{
    public class Customer
    {
        public string? Customer_Ref {get;set;}
        public string? Customer_Name {get;set;}
        public string? Address_Line_1 {get;set;}
        public string? Address_Line_2 {get;set;}
        public string? Town {get;set;}
        public string? County {get;set;}
        public string? Country {get;set;}
        public string? Postcode { get; set; }

        [JsonConstructor] public Customer() { }

        public Customer(string customer_Ref, string customer_Name, string address_Line_1, string address_Line_2, string town, string county, string country, string postcode)
        {
            Customer_Ref = customer_Ref;
            Customer_Name = customer_Name;
            Address_Line_1 = address_Line_1;
            Address_Line_2 = address_Line_2;
            Town = town;
            County = county;
            Country = country;
            Postcode = postcode;
        }
    }
}
