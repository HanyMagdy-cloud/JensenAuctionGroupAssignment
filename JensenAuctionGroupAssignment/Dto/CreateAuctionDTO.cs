namespace JensenAuctionGroupAssignment.Dto
{
    // Data Transfer Object for creating a new auction
    public class CreateAuctionDTO
    {
        public string Title { get; set; } // Title of the auction
        public string Description { get; set; } // Description of the auction
        public decimal Price { get; set; } // Starting price of the auction
        public DateTime StartDate { get; set; } // Start date and time of the auction
        public DateTime EndDate { get; set; } // End date and time of the auction
        public int CreatedBy { get; set; } // User ID of the creator
    }
}
