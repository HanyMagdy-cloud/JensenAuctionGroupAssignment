namespace JensenAuctionGroupAssignment.Dto
{
    public class AuctionDTO
    {
        public int AuctionID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal StartingPrice { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } // Timestamp for when the auction was created

    }
}
