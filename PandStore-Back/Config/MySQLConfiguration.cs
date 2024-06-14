using System.Configuration;

namespace PandStore_Back.Config
{
    public class MySQLConfiguration
    {
        public MySQLConfiguration(string connectionString) { 
        ConnectionString = connectionString;
    }
        public string ConnectionString { get; set; }
    }
}
