namespace JensenAuctionGroupAssignment.Entites
{
    // Represents the Auction entity in the database

    public class Auction
    {
        public int AuctionID { get; set; } // Primary key for the Auctions table
        public string Title { get; set; } // Title of the auction
        public string Description { get; set; } // Description of the auction
        public decimal StartingPrice { get; set; } // Starting price of the auction
        public DateTime StartDate { get; set; } // Start date and time of the auction
        public DateTime EndDate { get; set; } // End date and time of the auction
        public int CreatedBy { get; set; } // Foreign key referencing the user who created the auction
        public DateTime CreatedAt { get; set; } = DateTime.Now; // Date and time the auction was created
    }
}
