using Aibidia.API.Common.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aibidia.API.Common.Repositories
{
    public class DbContextFactory
    {
        public static ApplicationDbContext Create()
        {
            string connStrBase = ConfigurationManager.ConnectionStrings["DbBase"]?.ConnectionString;
            if (connStrBase == null)
            {
                Trace.TraceError("No connection string base set.");
                throw new Exception("No connection string base set.");
            }

            var connStrBld = new SqlConnectionStringBuilder(connStrBase);
            var context = new ApplicationDbContext(connStrBld.ConnectionString);
            return context;
        }
    }
}
