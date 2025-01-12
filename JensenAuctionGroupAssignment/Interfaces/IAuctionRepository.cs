using JensenAuctionGroupAssignment.Dto;
using JensenAuctionGroupAssignment.Entites;

namespace JensenAuctionGroupAssignment.Interfaces
{
    // Interface for auction-related operations
    public interface IAuctionRepository
    {
        

        int CreateAuction(CreateAuctionDTO auctionDTO); // Accepts DTO for creating a new auction
        AuctionDTO GetAuctionById(int auctionId); // Returns DTO for retrieving an auction by its ID
        List<AuctionDTO> GetAllAuctions(); // Returns a list of DTOs for all auctions
        int UpdateAuctionByUserId(int auctionId, int userId, UpdateAuctionDTO auctionDTO);
        int DeleteAuctionByUserId(int auctionId, int userId);
        //AuctionWithHighestBidDTO GetAuctionAndHighestBid(int auctionId); // Use the
        List<AuctionDTO> SearchAuctions(string searchTerm); // Search auctions by title or description
        int UpdateAuction(int auctionId, UpdateAuctionDTO auctionDTO);
    }
}
