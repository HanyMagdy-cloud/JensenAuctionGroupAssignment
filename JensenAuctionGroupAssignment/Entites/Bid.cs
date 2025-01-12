namespace JensenAuctionGroupAssignment.Entites
{
    // Represents the Bid entity in the database
    public class Bid
    {
        public int BidID { get; set; } // Primary key for the Bids table
        public int AuctionID { get; set; } // Foreign key referencing the auction
        public int UserID { get; set; } // Foreign key referencing the user who placed the bid
        public decimal BidAmount { get; set; } // Amount of the bid
        public DateTime CreatedAt { get; set; } = DateTime.Now; // Date and time the bid was placed
    }
}
