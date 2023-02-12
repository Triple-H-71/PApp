﻿using Microsoft.AspNetCore.Mvc;
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
                // Create a query that retrieves all books with an author name of "John Smith"    
                var sql = "SELECT * FROM Sales";

                // Use the Query method to execute the query and return a list of objects    
                sales = connection.Query<Sale>(sql).ToList();
            }

            return sales;
        }
    }
}
