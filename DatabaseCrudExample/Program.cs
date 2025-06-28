//using DatabaseCrudExample;

//Console.WriteLine("Hello World");
//string connectionString = "Server=2404NB600260\\SQLEXPRESS;Database=TestDb;User Id=sa;Password=hpp;TrustServerCertificate=True;";

//// Create an instance of AdoNetOperations and perform CRUD
//AdoNetOperations operations = new AdoNetOperations(connectionString);
//operations.PerformCrud();

//Console.WriteLine("\nPress any key to exit...");
//Console.ReadKey();

//using DatabaseCrudExample;

//string connectionString = "Server=2404NB600260\\SQLEXPRESS;Database=TestDb;User Id=sa;Password=hpp;TrustServerCertificate=True;";

//// Create an instance of DapperOperations
//DapperOperations dapperOps = new DapperOperations(connectionString);

//// Perform CRUD operations using Dapper
//dapperOps.PerformCrud();

//Console.WriteLine("\nPress any key to exit...");
//Console.ReadKey();

using DatabaseCrudExample;

// Run EF Core CRUD operations
EfCoreOperations efCoreOps = new EfCoreOperations();
efCoreOps.PerformCrud();

Console.WriteLine("\nPress any key to exit...");
Console.ReadKey();