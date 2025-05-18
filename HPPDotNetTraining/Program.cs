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
//Console.Write("Enter Username: ");
//string userName = Console.ReadLine();
//Console.Write("Enter Password: ");
//string password = Console.ReadLine();
//var request = new
//{
//    UserName = userName,
//    Password= password
//};

//SqlConnectionStringBuilder stringBuilder = new SqlConnectionStringBuilder
//{
//    DataSource = "2404NB600260\\SQLEXPRESS",
//    InitialCatalog = "HPPDotNetTraining",
//    UserID = "sa",
//    Password = "hpp",
//    TrustServerCertificate=true,

//};
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
//using IDbConnection db = new SqlConnection(stringBuilder.ConnectionString);
//db.Open();
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

//string message = result > 0 ? "Saving Successful" : "Saving Failed";
//   string query = $"Select * from Tbl_User where UserName=@UserName and Password=@Password";
//var lst = db.Query(query,request).ToList();
//SqlConnection connection = new SqlConnection(stringBuilder.ConnectionString);
//connection.Open();
//string query = $"Select * from Tbl_User where UserName=@UserName and Password=@Password";
//SqlCommand cmd=new SqlCommand(query, connection);
//cmd.Parameters.AddWithValue("@UserName", userName);
//cmd.Parameters.AddWithValue("@Password", password);

//SqlDataAdapter adapter=new SqlDataAdapter(cmd);
//DataTable dt=new DataTable();
//adapter.Fill(dt);
//connection.Close();
//AppDbContext db = new AppDbContext();
AppDbContext db = new AppDbContext();
//Select
//var lst = db.Users.ToList();

//foreach (var item in lst)
//{
//    Console.WriteLine(item.UserName);
//    Console.WriteLine(item.Password);
//}

//Create
//Console.Write("Enter Username: ");
//string userName = Console.ReadLine();
//Console.Write("Enter Password: ");
//string password = Console.ReadLine();

//UserDto dto = new UserDto
//{
//    UserName = userName,
//    Password = password,
//    UserId = Guid.NewGuid().ToString()
//};
//db.Users.Add(dto);
//int result=db.SaveChanges();
//Update
Console.WriteLine("Enter UserId: ");
string userID=Console.ReadLine();
var item= db.Users.Where(x => x.UserId == userID).FirstOrDefault();
if(item is null)
{
    Console.WriteLine("No data Found.");
    return;
}
item.UserName = "Test4";
item.Password = "test";
db.SaveChanges();
//Delete
//db.Users.Remove(item);
//db.SaveChanges();
Console.ReadLine();

