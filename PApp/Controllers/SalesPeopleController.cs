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
    [Route("SalesPerson")]
    public class SalesPeopleController : ControllerBase
    {      

        private readonly ILogger<SalesPeopleController> _logger;
        private readonly IDBConnector _dbc;
        

        public SalesPeopleController(ILogger<SalesPeopleController> logger, IDBConnector dbc)
        {
            _logger = logger;
            _dbc = dbc;
        }
                
        [HttpGet]
        public List<Salesperson> Get()
        {
            List<Salesperson> salesPpl;
            // var salesPpl = new List<Salesperson>();
            using (var connection = new SqlConnection(_dbc.GetDatabaseName()))
            {
                // Create a query that retrieves all books with an author name of "John Smith"    
                var sql = "SELECT * FROM Salesperson";

                // Use the Query method to execute the query and return a list of objects    
                salesPpl = connection.Query<Salesperson>(sql).ToList();
            }

            return salesPpl;            
        }
    }
}