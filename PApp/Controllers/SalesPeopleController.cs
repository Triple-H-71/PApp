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
            List<Salesperson> salesperson;
            
            using (var connection = new SqlConnection(_dbc.GetDatabaseName()))
            {
                // Create a query that retrieves all books with an author name of "John Smith"    
                var sql = "SELECT * FROM Salesperson";

                // Use the Query method to execute the query and return a list of objects    
                salesperson = connection.Query<Salesperson>(sql).ToList();
            }

            return salesperson;            
        }

        [HttpPut]
        public Task<bool> UpdateSalesperson(Salesperson salesperson)
        {
            try
            {
                if (salesperson == null)
                    return Task.FromResult(false);

                var sql = @"UPDATE [dbo].[Salesperson] SET " +
                           "   [FirstName] = '" + salesperson.FirstName + "'" +
                           "   ,[Lastname] = '" + salesperson.LastName + "'" +
                           "   ,[Address] =  '" + salesperson.Address + "'" +
                           "   ,[Phone] =  '" + salesperson.Phone + "'" +
                           "   ,[StartDate] =  '" + salesperson.StartDate + "'" +
                           "   ,[TerminationDate] = '" + salesperson.TerminationDate + "'" +
                           "   ,[Manager] = '" + salesperson.Manager + "'" +
                           "   WHERE id=" + salesperson.Id;

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