using Dapper;
using HPPDotNetTraining;
using Microsoft.Data.SqlClient;
using System.Data;


//string query = @"SELECT [Id]
//      ,[Name]
//      ,[Quantity]
//      ,[Price]
//      ,[CreateDateTime]
//      ,[ModifiedDateTime]
//  FROM [dbo].[Tbl_Product]";

//string query = @"INSERT INTO [dbo].[Tbl_Product]
//           ([Name]
//           ,[Quantity]
//           ,[Price]
//           ,[CreateDateTime])
//     VALUES('Banana',3,4000,GETDATE())";

//string query = @"UPDATE [dbo].[Tbl_Product]
//    SET CreateDateTime=GETDATE()
//    WHERE Id=2";

//String query = @"DELETE FROM [dbo].[Tbl_Product]
//      WHERE Id=3";

SqlConnectionStringBuilder stringBuilder = new SqlConnectionStringBuilder
{
    DataSource = "2404NB600260\\SQLEXPRESS",
    InitialCatalog = "HPPDotNetTraining",
    UserID = "sa",
    Password = "hpp",
    TrustServerCertificate=true,

};
//SqlConnection connection = new SqlConnection(stringBuilder.ConnectionString);
//Console.WriteLine("Connection Opening....");
//connection.Open();
//Console.WriteLine("Connection Opened.");

//Console.WriteLine("Connection Closing....");
//connection.Close();
//Console.WriteLine("Connection Closed");
//Console.ReadLine();



//connection.Open();
//SqlCommand cmd = new SqlCommand(query, connection);
//SqlDataAdapter adapter = new SqlDataAdapter(cmd);
//DataTable dt= new DataTable();
//adapter.Fill(dt);

//int result=cmd.ExecuteNonQuery();
//Console.WriteLine(result == 1 ? "Saving Successful":"Saving Failed");
//connection.Close();
//for (int i = 0; i < dt.Rows.Count; i++)
//{   
//    DataRow dr = dt.Rows[i];
//    Console.WriteLine(dr["Id"]);
//    Console.WriteLine(dr["Name"]);
//    Console.WriteLine(dr["Quantity"]);
//    Console.WriteLine(dr["Price"]);
//    Console.WriteLine(dr["CreateDateTime"]);
//    Console.WriteLine("========================");

//}
//Console.ReadLine();
using IDbConnection db= new SqlConnection(stringBuilder.ConnectionString);
db.Open();
//List<Product>lst= db.Query<Product>("select * from tbl_product").ToList();
//foreach (Product item in lst)
//{
//    Console.WriteLine(item.Name);
//    Console.WriteLine(item.Price);
//}
//string query = @"INSERT INTO [dbo].[Tbl_Product]
//           ([Name]
//           ,[Quantity]
//           ,[Price]
//           ,[CreateDateTime])
//     VALUES('Mango',3,1000,GETDATE())";

//string query = @"UPDATE [dbo].[Tbl_Product]
//    SET CreateDateTime='2025-5-10'
//    WHERE Id=2";

//String query = @"DELETE FROM [dbo].[Tbl_Product]
//      WHERE Id=4";
//int result = db.Execute(query);

string message = result > 0 ? "Saving Successful" : "Saving Failed";

Console.ReadLine();

