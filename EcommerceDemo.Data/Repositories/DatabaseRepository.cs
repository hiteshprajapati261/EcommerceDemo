using EcommerceDemo.Data.Interfaces;
using System.Configuration;

namespace EcommerceDemo.Data.Repositories
{
    public class DatabaseRepository : IDatabaseRepository
    {
        public DatabaseRepository()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["ECommerceDemoConnectionString"].ConnectionString;
        }

        public string ConnectionString { get; private set; }
    }
}
