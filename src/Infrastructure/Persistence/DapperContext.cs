using System.Data;
using Microsoft.Data.SqlClient;

namespace REA.Accounting.Infrastructure.Persistence
{
    public class DapperContext
    {
        private readonly string _connectionStr;

        public DapperContext(string connStr) => _connectionStr = connStr;

        public IDbConnection CreateConnection() => new SqlConnection(_connectionStr);
    }
}