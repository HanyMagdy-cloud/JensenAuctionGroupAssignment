using Microsoft.Data.SqlClient;

namespace JensenAuctionGroupAssignment.Interfaces
{
    public interface IJensenAuction
    {
        // Interface to provide a method for retrieving a database connection
        
        
            // Method to get an open SQL connection
            public SqlConnection GetConnection();
        
    }
}

