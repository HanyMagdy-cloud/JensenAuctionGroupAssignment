namespace JensenAuctionGroupAssignment.Dto
{
    public class UpdateAuctionDTO
    {
        public string Title { get; set; } // Updated auction title
        public string Description { get; set; } // Updated auction description
        public decimal StartingPrice { get; set; } // Updated starting price
        public DateTime StartDate { get; set; } // Updated start date
        public DateTime EndDate { get; set; } // Updated end date
    }
}
