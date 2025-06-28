using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPPDotNetTraining.WindowsFromApp
{
    public class UserRepository
    {
        public void AddUser(User user)
        {
            string sql = @"INSERT INTO Users (UserName, Password)
                           VALUES (@UserName, @Password)";
            using var connection = DbHelper.GetConnection();
            connection.Execute(sql, user);
        }

        public void UpdateUser(User user)
        {
            string sql = @"UPDATE Users 
                           SET UserName = @UserName, Password = @Password 
                           WHERE UserId = @UserId";
            using var connection = DbHelper.GetConnection();
            connection.Execute(sql, user);
        }
        public void DeleteUser(int userId)
        {
            string sql = "DELETE FROM Users WHERE UserId = @UserId";
            using var connection = DbHelper.GetConnection();
            connection.Execute(sql, new { UserId = userId });
        }

        public User GetUserById(int userId)
        {
            string sql = "SELECT * FROM Users WHERE UserId = @UserId";
            using var connection = DbHelper.GetConnection();
            return connection.QuerySingleOrDefault<User>(sql, new { UserId = userId });
        }

        public IEnumerable<User> GetAllUsers()
        {
            string sql = "SELECT * FROM Users";
            using var connection = DbHelper.GetConnection();
            return connection.Query<User>(sql).ToList();
        }
    }
}
