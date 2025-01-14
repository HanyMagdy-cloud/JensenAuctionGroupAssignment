namespace JensenAuctionGroupAssignment;

public class UnitTest1
{
    [Fact]
    public void AuctionRepositoryTests
    {
            private readonly AuctionRepository _auctionRepo; // Instance of the repository being tested
    public AuctionRepositoryTests()
    {
        // Initialize the repository with a real database context or a pre-configured test database
        var dbContext = new JensenContext(); // Replace with your actual database context
        _auctionRepo = new AuctionRepository(dbContext); // Pass the context to the repository
    }

    [Fact] // This marks the method as a test case
    public void GetAuctionById_ValidAuctionId_ReturnsAuction()
    {
        // Arrange: Set up the input for the test
        int validAuctionId = 1; // Replace with a valid AuctionID from the database

        // Act: Call the method being tested
        var auction = _auctionRepo.GetAuctionById(validAuctionId);

        // Assert: Verify the expected outcome
        Assert.NotNull(auction); // Check that the auction is not null
        Assert.Equal(validAuctionId, auction.AuctionID); // Check that the AuctionID matches the input
    }

}
}