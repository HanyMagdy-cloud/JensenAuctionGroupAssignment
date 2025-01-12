using JensenAuctionGroupAssignment.Entites;

namespace JensenAuctionGroupAssignment.Interfaces
{
    // Interface for bid-related operations
    public interface IBidRepository
    {
        int PlaceBid(Bid bid); // Places a new bid and returns the BidID
        List<Bid> GetBidsForAuction(int auctionId); // Retrieves all bids for a specific auction
        Bid GetHighestBidForAuction(int auctionId); // Retrieves the highest bid for a specific auction
        int DeleteBidById(int bidId);
        //object GetAuctionAndHighestBid(int auctionId);
    }
}
