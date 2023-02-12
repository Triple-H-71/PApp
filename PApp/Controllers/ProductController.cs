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
        public List<Product> Get()
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
        [HttpPut]
        public Task<bool> UpdateProduct(Product product)
        {
            try
            {
                if (product == null)
                    return Task.FromResult(false);

                var sql = @"UPDATE [dbo].[Product] SET " +
                           "   [Name] = '" + product.Name + "'" +
                           "   ,[Manufacturer] = '" + product.Manufacturer + "'" +
                           "   ,[Style] =  '" + product.Style + "'" +
                           "   ,[PurchasePrice] =  '" + product.PurchasePrice + "'" +
                           "   ,[QtyOnHand] =  '" + product.QtyOnHand + "'" +
                           "   ,[CommissionPercentage] = '" + product.CommissionPercentage + "'" +                           
                           "   WHERE id=" + product.Id;

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
