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
    [Route("Sale")]
    public class SaleController : ControllerBase
    {
        private readonly ILogger<SaleController> _logger;
        private readonly IDBConnector _dbc;

        public SaleController(ILogger<SaleController> logger, IDBConnector dbc)
        {
            _logger = logger;
            _dbc = dbc;
        }

        [HttpGet]
        public List<Sale> Get()
        {
            List<Sale> sales;

            using (var connection = new SqlConnection(_dbc.GetDatabaseName()))
            {                  
                var sql = "SELECT * FROM Sales";             
                sales = connection.Query<Sale>(sql).ToList();
            }

            return sales;
        }

        [HttpPost]
        public Task<bool> AddSale (Sale sale)
        {
            try
            {
                if (sale == null)
                    return Task.FromResult(false);
                sale.Id = GetNextCount();
                if (CheckProductExists(sale.ProductId) && CheckSalespersonExists(sale.SalesPersonId) && CheckCustomerExists(sale.CustomerId) )
                {
                    var sql = @"INSERT INTO [dbo].[Sales] ([ID],[ProductID],[SalesPersonID],[CustomerID],[SalesDate]) VALUES (" +
                            sale.Id + "," +
                            sale.ProductId + "," +
                           "'" + sale.SalesPersonId + "'," +
                           "'" + sale.CustomerId + "'," +
                           "'" + sale.SalesDate + "'" +
                            ")";
                    using (var connection = new SqlConnection(_dbc.GetDatabaseName()))
                    {
                        var affectedRows = connection.Execute(sql);

                        // Console.WriteLine($"Affected Rows: {affectedRows}");
                    }
                    return Task.FromResult(true);
                }                              
            }
            catch (Exception)
            {
                return Task.FromResult(false);
            }

            return Task.FromResult(false);
        }

        private bool CheckProductExists(int productID)
        {
            try
            {
                using (var connection = new SqlConnection(_dbc.GetDatabaseName()))
                {

                    var sql = "select ID from Product where ID=" + productID;

                    var iVal = connection.Query(sql);
                    if (iVal.Count() > 0)
                        return true;
                    //Debug.WriteLine(maxCount);
                }
            }
            catch (Exception)
            {

                throw;
            }

            return false;
        }
        private bool CheckSalespersonExists(int salespersonId)
        {
            try
            {
                using (var connection = new SqlConnection(_dbc.GetDatabaseName()))
                {

                    var sql = "select ID from Salesperson where ID=" + salespersonId;

                    var iVal = connection.Query(sql);
                    if (iVal.Count() > 0)
                        return true;
                    //Debug.WriteLine(maxCount);
                }
            }
            catch (Exception)
            {

                throw;
            }

            return false;
        }

        private bool CheckCustomerExists(int customerId)
        {
            try
            {
                using (var connection = new SqlConnection(_dbc.GetDatabaseName()))
                {

                    var sql = "select ID from Customer where ID=" + customerId;

                    var iVal = connection.Query(sql);
                    if (iVal.Count() > 0)
                        return true;
                    //Debug.WriteLine(maxCount);
                }
            }
            catch (Exception)
            {

                throw;
            }

            return false;
        }

        private int GetNextCount()
        {
            var maxCount = 0;            

            try
            {
                using (var connection = new SqlConnection(_dbc.GetDatabaseName()))
                {
                    
                    var sql = "select max(id)+1 as MaxCount from Sales";

                    var iVal = connection.QuerySingle(sql);
                    if (iVal != null)      
                        maxCount = iVal.MaxCount;
                    Debug.WriteLine(maxCount);
                }
            }
            catch (Exception)
            {

                throw;
            }

            return maxCount;

        }
    }
}
