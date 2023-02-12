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
    [Route("Product")]
    public class ProductController : ControllerBase
    {
        private readonly IDBConnector _dbc;
        public ProductController(IDBConnector dbc)
        {
            _dbc = dbc;
        }

        [HttpGet(Name = "Product")]
        public List<Product> GetAllProducts()
        {
            List<Product> products;
            // var salesPpl = new List<Salesperson>();
            using (var connection = new SqlConnection(_dbc.GetDatabaseName()))
            {
                // Create a query that retrieves all books with an author name of "John Smith"    
                var sql = "SELECT * FROM Product";

                // Use the Query method to execute the query and return a list of objects    
                products = connection.Query<Product>(sql).ToList();
            }

            return products;
        }
    }
}
