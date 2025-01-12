using Dapper;
using JensenAuctionGroupAssignment.Dto;
using JensenAuctionGroupAssignment.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace JensenAuctionGroupAssignment.Entites.Repo
{
    public class PlaceBidRepository : IBidRepository
    {
        private readonly IJensenAuction _dbContext; // Dependency to retrieve the database connection

        // Constructor to inject the IJensenAuction dependency

        public PlaceBidRepository(IJensenAuction dbContext)
        {
            _dbContext = dbContext;
        }

        // Place a bid with validation
        public int PlaceBid(Bid bid)
        {
            using (var connection = _dbContext.GetConnection())
            {
                connection.Open();

                try
                {
                    // Call the stored procedure and get the generated BidID
                    var bidId = connection.ExecuteScalar<int>(
                        "dbo.PlaceBid",
                        new
                        {
                            bid.AuctionID, // Auction ID
                            bid.UserID, // User ID
                            bid.BidAmount // Bid Amount
                        },
                        commandType: CommandType.StoredProcedure
                    );

                    return bidId; // Return the generated BidID
                }
                catch (SqlException ex)
                {
                    // Handle custom SQL exceptions
                    if (ex.Number == 50001)
                    {
                        throw new InvalidOperationException("The auction is closed. You cannot place a bid.");
                    }

                    if (ex.Number == 50002)
                    {
                        throw new InvalidOperationException("Your bid must be at least 10 SEK higher than the current highest bid or starting price.");
                    }

                    if (ex.Number == 50003)
                    {
                        throw new InvalidOperationException("You cannot place a bid on your own auction.");
                    }

                    if (ex.Number == 50004)
                    {
                        throw new InvalidOperationException("The auction does not exist.");
                    }

                    // Rethrow unexpected exceptions
                    throw;
                }
            }
        }


        // Retrieves all bids for a specific auction
        public List<Bid> GetBidsForAuction(int auctionId)
        {
            using (var connection = _dbContext.GetConnection())
            {
                connection.Open();

                return connection.Query<Bid>(
                    "dbo.GetBidsForAuction",
                    new { AuctionID = auctionId },
                    commandType: CommandType.StoredProcedure
                ).AsList();
            }
        }

        // Retrieves the highest bid for a specific auction
        public Bid? GetHighestBidForAuction(int auctionId)
        {
            using (var connection = _dbContext.GetConnection())
            {
                connection.Open();

                return connection.QueryFirstOrDefault<Bid>(
                    "dbo.GetHighestBidForAuction",
                    new { AuctionID = auctionId },
                    commandType: CommandType.StoredProcedure
                );
            }

        }

        // Helper method to retrieve the creator of an auction
        private int GetAuctionCreator(int auctionId)
        {
            using (var connection = _dbContext.GetConnection())
            {
                connection.Open();

                // Query to retrieve the CreatedBy (user ID of the auction creator)
                return connection.ExecuteScalar<int>(
                    "SELECT CreatedBy FROM Auctions WHERE AuctionID = @AuctionID",
                    new { AuctionID = auctionId }
                );
            }
        }

        // Deletes a bid by its BidID
        public int DeleteBidById(int bidId)
        {
            using (var connection = _dbContext.GetConnection()) // Get the database connection
            {
                connection.Open(); // Open the connection

                // Execute the stored procedure and return the number of rows affected
                return connection.Execute(
                    "dbo.DeleteBidById", // Stored procedure name
                    new { BidID = bidId }, // Pass the BidID as a parameter
                    commandType: CommandType.StoredProcedure // Specify that we're calling a stored procedure
                );
            }
        }

        
    }
}
