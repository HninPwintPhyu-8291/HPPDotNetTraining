using Microsoft.Data.SqlClient;
using System.Data;

namespace HPPDotNetTraining.BlogRepositoryDapper
{
    public static class DbHelper
    {
        private static readonly string connectionString = new SqlConnectionStringBuilder
        {
            DataSource = "2404NB600260\\SQLEXPRESS",
            InitialCatalog = "HPPDotNetTraining",
            UserID = "sa",
            Password = "hpp",
            TrustServerCertificate = true
        }.ConnectionString;

        public static IDbConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}