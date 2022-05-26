using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Microservice.Models;
using System.Data;
using System.Data.SqlClient;
using Dapper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Client.Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly string connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=ClientsDb;Integrated Security=True";

        // GET: api/<ClientController>
        [HttpGet]
        public async Task<IEnumerable<Models.Client>> GetAllClients()
        {
            IEnumerable<Models.Client> clients;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var sqlQuery = "Select * From Client";
                clients = await connection.QueryAsync<Models.Client>(sqlQuery);
            }
            return clients;

        }

        // GET api/<ClientController>/5
        [HttpGet("{id}")]
        public async Task<Models.Client> GetClientById(Guid id)
        {   
            Models.Client client = new Models.Client();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var sqlQuery = "Select * From Client Where Id = @Id";
                client = await connection.QuerySingleAsync<Models.Client>(sqlQuery, new { Id = id });
            }

            return client;
        }

        // POST api/<ClientController>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Models.Client client)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var sqlQuery = "Insert Into Client (id, firstName, lastName, birthdate, address, email) Values(newid(), @firstName, @lastName, @DateOfBirth, @address, @email)";
                await connection.ExecuteAsync(sqlQuery, client);
            }
            return Ok();

        }

        // PUT api/<ClientController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Models.Client client)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var sqlQuery = "UPDATE Client SET firstName = @firstName, lastName = @lastName, birthdate = @dateOfBirth, address = @address, email = @email WHERE Id = @Id";
                await connection.ExecuteAsync(sqlQuery, client);
            }
            return Ok();
        }

        // DELETE api/<ClientController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var sqlQuery = "Delete Client Where Id = @Id";
                await connection.ExecuteAsync(sqlQuery, new { Id = id });
            }
            return Ok();
        }
    }
}