using ESG_CSVreader;
using ESG_customerAPI.Data;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace ESG_customerAPI.Controllers
{
    [Route("Customers")]
    public class CustomersController : Controller
    {
        DatabaseWrapper customersWrapper;

        public CustomersController()
        {
            customersWrapper = new DatabaseWrapper();
        }

        [HttpPost]
        [EndpointName("save")]
        public ContentResult SaveCustomers([FromBody] IEnumerable<Customer> customers)
        {
            customersWrapper.Save(customers);

            return new ContentResult()
            {
                StatusCode = (int)HttpStatusCode.OK,
                ContentType = "application/json; charset=UTF-8",
                Content = JsonSerializer.Serialize(customers),
            };
        }

        [HttpGet]
        [EndpointName("read")]
        public JsonResult ReadCustomers(string customer_reference)
        {
            return new JsonResult(customersWrapper.Read(customer_reference));
        }
    }
}
