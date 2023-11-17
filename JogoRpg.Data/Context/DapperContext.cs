using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace JogoRpg.Data.Context
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionstring;

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionstring = _configuration.GetConnectionString("SqlConnection");
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionstring);
    }
}