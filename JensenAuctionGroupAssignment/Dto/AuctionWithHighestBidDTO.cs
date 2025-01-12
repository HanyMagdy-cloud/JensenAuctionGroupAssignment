namespace JensenAuctionGroupAssignment.Dto
{
    public class AuctionWithHighestBidDTO
    {
        public int AuctionID { get; set; } // Auction ID
        public string Title { get; set; } // Auction title
        public string Description { get; set; } // Auction description
        public decimal StartingPrice { get; set; } // Starting price
        public DateTime StartDate { get; set; } // Auction start date
        public DateTime EndDate { get; set; } // Auction end date
        public decimal? HighestBid { get; set; } // Highest bid amount (nullable)
        public int? HighestBidder { get; set; } // Highest bidder's user ID (nullable)
    }
}
