using Dapper;
using JensenAuctionGroupAssignment.Dto;
using JensenAuctionGroupAssignment.Interfaces;
using System.Data;

namespace JensenAuctionGroupAssignment.Entites.Repo
{
    // Implementation of IAuctionRepository
    public class AuctionRepository : IAuctionRepository
    {
        private readonly IJensenAuction _dbContext; // Dependency to retrieve the database connection

        // Constructor to inject the IJensenAuction dependency
        public AuctionRepository(IJensenAuction dbContext)
        {
            _dbContext = dbContext;  // Assign the injected dependency to the private field
        }

        // Creates a new auction using a stored procedure and returns the AuctionID
        public int CreateAuction(CreateAuctionDTO auctionDTO)
        {
            using (var connection = _dbContext.GetConnection()) // Get a database connection
            {
                connection.Open(); // Open the connection

                // Call the stored procedure to insert a new auction
                return connection.ExecuteScalar<int>(
                    "dbo.CreateAuction", // Stored procedure name
                    new
                    {
                        auctionDTO.Title, // Map DTO Title to stored procedure parameter
                        auctionDTO.Description, // Map DTO Description
                        auctionDTO.Price, // Map DTO StartingPrice
                        auctionDTO.StartDate, // Map DTO StartDate
                        auctionDTO.EndDate, // Map DTO EndDate
                        auctionDTO.CreatedBy // Map DTO CreatedBy (User ID)
                    },
                    commandType: CommandType.StoredProcedure // Specify this is a stored procedure
                );
            }
        }

        // Retrieves an auction by its ID using a stored procedure
        public AuctionDTO? GetAuctionById(int auctionId)
        {
            using (var connection = _dbContext.GetConnection()) // Get a database connection
            {
                connection.Open(); // Open the connection

                // Execute the stored procedure and map the result to AuctionDTO
                return connection.QueryFirstOrDefault<AuctionDTO>(
                    "dbo.GetAuctionById",

                    new { AuctionID = auctionId }, // Pass AuctionID parameter to the stored procedure
                    commandType: CommandType.StoredProcedure // Specify this is a stored procedure
                );
            }
        }

        // Retrieves all auctions using a stored procedure
        public List<AuctionDTO> GetAllAuctions()
        {
            using (var connection = _dbContext.GetConnection()) // Get a database connection
            {
                connection.Open(); // Open the connection

                // Execute the stored procedure and map the result to a list of AuctionDTOs
                return connection.Query<AuctionDTO>(
                    "dbo.GetAllAuctions", // Stored procedure name
                    commandType: CommandType.StoredProcedure // Specify this is a stored procedure
                ).AsList(); // Convert the IEnumerable<AuctionDTO> to List<AuctionDTO>
            }
        }


        public int UpdateAuctionByUserId(int auctionId, int userId, UpdateAuctionDTO auctionDTO)
        {
            using (var connection = _dbContext.GetConnection())
            {
                connection.Open();

                // Call the stored procedure
                var rowsAffected = connection.Execute(
                    "dbo.UpdateAuctionByUserId",
                    new
                    {
                        AuctionId = auctionId,
                        UserId = userId,
                        Title = auctionDTO.Title,
                        Description = auctionDTO.Description,
                        StartDate = auctionDTO.StartDate,
                        EndDate = auctionDTO.EndDate,
                        StartingPrice = auctionDTO.StartingPrice // Pass StartingPrice regardless of bid status
                    },
                    commandType: CommandType.StoredProcedure
                );

                return rowsAffected;
            }
        }


        public int DeleteAuctionByUserId(int auctionId, int userId)
        {
            using (var connection = _dbContext.GetConnection()) // Get a database connection
            {
                connection.Open(); // Open the connection

                // Call the stored procedure
                return connection.Execute(
                    "dbo.DeleteAuctionByUserId", // Stored procedure name
                    new
                    {
                        AuctionID = auctionId, // Pass AuctionID
                        UserId = userId // Pass UserId
                    },
                    commandType: CommandType.StoredProcedure // Specify that it's a stored procedure
                );
            }
        }



        // Search auctions by title or description
        public List<AuctionDTO> SearchAuctions(string searchTerm)
        {
            using (var connection = _dbContext.GetConnection()) // Get database connection
            {
                connection.Open(); // Open the connection

                // Execute stored procedure and map results to a list of AuctionDTO
                var auctions = connection.Query<AuctionDTO>(
                    "dbo.SearchAuctions", // Stored procedure name
                    new { SearchTerm = searchTerm }, // Pass the search term parameter
                    commandType: CommandType.StoredProcedure // Indicate it is a stored procedure
                ).AsList();

                return auctions; // Return the list of matching auctions
            }
        }

        public int UpdateAuction(int auctionId, UpdateAuctionDTO auctionDTO)
        {
            using (var connection = _dbContext.GetConnection()) // Get database connection
            {
                connection.Open(); // Open the connection

                // Execute the stored procedure
                var rowsAffected = connection.Execute(
                    "dbo.UpdateAuction", // Stored procedure name
                    new
                    {
                        AuctionID = auctionId, // Pass AuctionID
                        Title = auctionDTO.Title, // Pass Title
                        Description = auctionDTO.Description, // Pass Description
                        //StartingPrice = auctionDTO.StartingPrice, // Pass StartingPrice
                        StartDate = auctionDTO.StartDate, // Pass StartDate
                        EndDate = auctionDTO.EndDate // Pass EndDate
                    },
                    commandType: CommandType.StoredProcedure // Specify that it's a stored procedure
                );

                return rowsAffected; // Return the number of rows affected
            }
        }








    }

}


