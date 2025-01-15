using JensenAuctionGroupAssignment.Interfaces;
using Microsoft.Data.SqlClient;

namespace JensenAuctionGroupAssignment
{
    public class JensenContext : IJensenAuction
    {
        // Private field to hold the connection string for the database.
        private readonly string _connString;

        public JensenContext()
        {
            _connString = string.Empty; // Avoid null references
        }

        public JensenContext(IConfiguration config)
        {
            // Get the connection string from the configuration and ensure it is valid

            _connString = config.GetConnectionString("Jensen")
            ?? throw new InvalidOperationException("Connection string 'Jensen' not found.");
        }

        public SqlConnection GetConnection()
        {
            // Ensure the connection string is initialized before returning the connection
            //if (string.IsNullOrEmpty(_connString))
            //{
            //    throw new InvalidOperationException("The connection string has not been initialized.");
            //}

            //return new SqlConnection(_connString);

            if (string.IsNullOrEmpty(_connString))
            {
                Console.WriteLine("Connection string is not initialized.");
                throw new InvalidOperationException("The connection string has not been initialized.");
            }

            try
            {
                var connection = new SqlConnection(_connString);
                Console.WriteLine("Successfully created a database connection.");
                return connection;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error creating connection: " + ex.Message);
                throw;
            }
        }
    }
}
