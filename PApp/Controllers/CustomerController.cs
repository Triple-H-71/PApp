using Microsoft.AspNetCore.Mvc;
using Dapper;
using System.Diagnostics;
using PApp.Models;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Cors;

namespace PApp.Controllers
{
    [EnableCors("MyPolicy")]
    [ApiController]
    [Route("Customer")]
    public class CustomerController : Controller
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly IDBConnector _dbc;

        public CustomerController(ILogger<CustomerController> logger, IDBConnector dbc)
        {
            _logger = logger;
            _dbc = dbc;
        }

        [HttpGet]
        public List<Customer> Get()
        {
            List<Customer> customers;
            // var salesPpl = new List<Salesperson>();
            using (var connection = new SqlConnection(_dbc.GetDatabaseName()))
            {
                // Create a query that retrieves all books with an author name of "John Smith"    
                var sql = "SELECT * FROM Customer";

                // Use the Query method to execute the query and return a list of objects    
                customers = connection.Query<Customer>(sql).ToList();
            }

            return customers;
        }
    }
}
