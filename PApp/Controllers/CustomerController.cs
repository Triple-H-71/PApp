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
    public class CustomerController : ControllerBase
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

        [HttpPut]
        public Task<bool> UpdateCustomer(Customer customer)
        {
            try
            {
                if (customer == null)
                    return Task.FromResult(false);

                var sql = @"UPDATE [dbo].[Customer] SET " +
                           "   [ProductID] = '" + customer.ProductId + "'" +
                           "   ,[FirstName] = '" + customer.FirstName + "'" +
                           "   ,[LastName] =  '" + customer.LastName + "'" +
                           "   ,[Address] =  '" + customer.Address + "'" +
                           "   ,[Phone] =  '" + customer.Phone + "'" +
                           "   ,[StartDate] = '" + customer.StartDate + "'" +                           
                           "   WHERE id=" + customer.Id;

                using (var connection = new SqlConnection(_dbc.GetDatabaseName()))
                {
                    var affectedRows = connection.Execute(sql);

                    // Console.WriteLine($"Affected Rows: {affectedRows}");
                }
                return Task.FromResult(true);
            }
            catch (Exception)
            {
                return Task.FromResult(false);
            }
        }
    }
}
