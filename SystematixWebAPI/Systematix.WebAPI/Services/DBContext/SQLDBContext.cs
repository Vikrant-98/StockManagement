using Microsoft.Data.SqlClient;

namespace Systematix.WebAPI.Services.DBContext
{
    public class SQLDBContext
    {
        private SqlConnection conn { get; }
        public SQLDBContext(string connectionString)
        {
            conn = new SqlConnection(connectionString);
        }
    }
}
