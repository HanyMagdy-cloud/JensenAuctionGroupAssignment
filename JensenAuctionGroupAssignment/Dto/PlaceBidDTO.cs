namespace JensenAuctionGroupAssignment.Dto
{
    // Data Transfer Object for placing a bid
    public class PlaceBidDTO
    {
        public int AuctionID { get; set; } // The ID of the auction being bid on
        public int UserID { get; set; } // The ID of the user placing the bid
        public decimal BidAmount { get; set; } // The amount of the bid
    }
}
