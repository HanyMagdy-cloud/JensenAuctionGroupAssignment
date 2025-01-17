using Dapper;
using JensenAuctionGroupAssignment.Interfaces;
using System.Data;

namespace JensenAuctionGroupAssignment.Entites.Repo
{
    public class UserRepository : IUserRepository
    {
        private readonly IJensenAuction _dbContext; // Interface for accessing database connection

        // Constructor to inject the database connection provider
        public UserRepository(IJensenAuction dbContext)
        {
            _dbContext = dbContext;
        }

        // Create a new user and return the generated UserId
        public int CreateUser(string username, string password)
        {
            using (var connection = _dbContext.GetConnection())
            {
                connection.Open();

                // Create a DynamicParameters object to pass parameters to the stored procedure

                var parameters = new DynamicParameters();
                parameters.Add("@Username", username);
                parameters.Add("@Password", password);
                parameters.Add("@UserId", dbType: DbType.Int32, direction: ParameterDirection.Output); // For output parameter

                connection.Execute("dbo.CreateUser", parameters, commandType: CommandType.StoredProcedure);

                return parameters.Get<int>("@UserId"); // Retrieve the output parameter value
            }
        }


        public User? GetUserByUsername(string username)
        {
            using (var connection = _dbContext.GetConnection())
            {
                connection.Open();

                // check the input username
                Console.WriteLine($"Input Username: {username}");

                // Execute the stored procedure
                var user = connection.QueryFirstOrDefault<User>(
                    "dbo.GetUserByUsername",
                    new { Username = username.Trim() }, // Trim the input
                    commandType: System.Data.CommandType.StoredProcedure
                );

                // check if the user was found or not
                Console.WriteLine(user != null ? "User found." : "User not found.");
                return user;
            }
        }
    }
}
