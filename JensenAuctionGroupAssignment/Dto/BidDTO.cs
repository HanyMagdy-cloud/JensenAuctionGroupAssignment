namespace JensenAuctionGroupAssignment.Dto
{
    public class BidDTO
    {
        public int BidID { get; set; }
        public int AuctionID { get; set; }
        public int UserID { get; set; }
        public decimal BidAmount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
