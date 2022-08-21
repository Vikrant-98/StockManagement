namespace Systematix.WebAPI.Services.DBContext
{
    public class SQLDBConnect
    {
        public readonly SQLDBContext _database;
        public SQLDBConnect(SQLDBContext database) 
        {
            _database = database;
        }
    }
}
