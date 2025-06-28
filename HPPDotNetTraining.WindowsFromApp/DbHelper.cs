using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPPDotNetTraining.WindowsFromApp
{
    public static class DbHelper
    {
        private static readonly string connectionString = new SqlConnectionStringBuilder
        {
            DataSource = "2404NB600260\\SQLEXPRESS", // Change if needed
            InitialCatalog = "HPPDotNetTraining",    // Your database name
            UserID = "sa",                           // SQL login
            Password = "hpp",                        // SQL password
            TrustServerCertificate = true
        }.ConnectionString;

        public static IDbConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
