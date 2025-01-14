using JensenAuctionGroupAssignment;
using JensenAuctionGroupAssignment.Entites.Repo;
using JensenAuctionGroupAssignment.Interfaces;
using Microsoft.Extensions.Configuration; // For ConfigurationBuilder
using Xunit; // xUnit for testing
using System.IO; // For Directory

namespace JensenAuction.Tests
{
    public class AuctionRepositoryTests
    {
        private readonly AuctionRepository _auctionRepo; // Repository being tested

        public AuctionRepositoryTests()
        {
            // Build configuration from appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // Ensure this points to the test project's directory
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) // Load appsettings.json
                .Build();

            // Initialize JensenContext with the configuration
            var jensenContext = new JensenContext(configuration);

            // Initialize the AuctionRepository with the JensenContext
            _auctionRepo = new AuctionRepository(jensenContext);
        }

        [Fact] // Marks the method as a test case
        public void GetAuctionById_ValidAuctionId_ReturnsAuction()
        {
            // Arrange: Set up the input for the test
            int validAuctionId = 1; // valid AuctionID from database

            // Act: Call the method being tested
            var auction = _auctionRepo.GetAuctionById(validAuctionId);

            // Assert: Verify the expected outcome
            Assert.NotNull(auction); // Check that the auction is not null
            Assert.Equal(validAuctionId, auction.AuctionID); // Check that the AuctionID matches the input
        }

        [Fact]
        public void GetAuctionById_InvalidAuctionId_ReturnsNull()
        {
            int invalidAuctionId = -1; // An invalid AuctionID that does not exist in the database

            var auction = _auctionRepo.GetAuctionById(invalidAuctionId); // Act: Call the method being tested

            Assert.Null(auction); // Assert: The result should be null for an invalid AuctionID
        }

        [Fact]
        public void SearchAuctions_ValidSearchTerm_ReturnsMatchingAuctions()
        {
            // Arrange: Set up the input for the test
            string searchTerm = "Antik Klocka"; // Valid term that exists in your database

            // Act: Call the method being tested
            var auctions = _auctionRepo.SearchAuctions(searchTerm);

            // Assert: Verify the expected outcome
            Assert.NotNull(auctions); // Ensure the result is not null
            Assert.NotEmpty(auctions); // Ensure the list is not empty
        }

        [Fact] 
        public void SearchAuctions_InvalidSearchTerm_ReturnsEmptyList()
        {
            // Arrange: Set up the input for the test
            string searchTerm = "BMW Car"; // Search term that does not exist in the database

            // Act: Call the method being tested
            var auctions = _auctionRepo.SearchAuctions(searchTerm);

            // Assert: Verify the expected outcome
            Assert.NotNull(auctions); // Ensure the result is not null
            Assert.Empty(auctions); // Ensure the list is empty for a non-existent term
        }


    }
}
