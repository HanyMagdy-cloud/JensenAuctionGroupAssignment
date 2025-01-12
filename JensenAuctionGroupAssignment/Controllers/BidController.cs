using JensenAuctionGroupAssignment.Dto;
using JensenAuctionGroupAssignment.Entites;
using JensenAuctionGroupAssignment.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JensenAuctionGroupAssignment.Controllers
{
    [Authorize] // Ensure the user is logged in
    [Route("api/[controller]")]
    [ApiController]
    public class BidController : ControllerBase
    {
        private readonly IBidRepository _bidRepository; // Dependency for bid-related operations

        // Constructor to inject the IBidRepository dependency
        public BidController(IBidRepository bidRepository)
        {
            _bidRepository = bidRepository; // Assign the injected dependency
        }

        
        [HttpPost("Place_Bid")]
        public IActionResult PlaceBid([FromQuery] PlaceBidDTO bidDTO)
        {
            try
            {
                // Create a new Bid object
                var bid = new Bid
                {
                    AuctionID = bidDTO.AuctionID, // Auction ID from request
                    UserID = bidDTO.UserID, // User ID from request
                    BidAmount = bidDTO.BidAmount // Bid Amount from request
                };

                // Place the bid
                var bidId = _bidRepository.PlaceBid(bid);

                return Ok(new { BidID = bidId, Message = "Bid placed successfully." });
            }
            catch (InvalidOperationException ex)
            {
                // Return a Bad Request for business logic errors
                return BadRequest(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                // Handle unexpected errors
                return StatusCode(500, new { Error = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        // Endpoint to get all bids for a specific auction
        [HttpGet("get_all_bids_for_a_specific_auction/{auctionId}")]
        public IActionResult GetBidsForAuction(int auctionId)
        {
            var bids = _bidRepository.GetBidsForAuction(auctionId);

            if (bids == null || bids.Count == 0)
            {
                return NotFound("No bids found for this auction.");
            }

            return Ok(bids);
        }


        // Endpoint to get the highest bid for a specific auction
        [HttpGet("Get_Highest_Bid_For_Auction/{auctionId}/depending_on_highest_Auction")]
        public IActionResult GetHighestBidForAuction(int auctionId)
        {
            var highestBid = _bidRepository.GetHighestBidForAuction(auctionId);

            if (highestBid == null)
            {
                return NotFound("No bids found for this auction.");
            }

            return Ok(highestBid);
        }

        // Endpoint to delete a bid by its BidID
        [HttpDelete("{bidId}")]
        public IActionResult DeleteBidById(int bidId)
        {
            try
            {
                // Call the repository method to delete the bid
                var rowsAffected = _bidRepository.DeleteBidById(bidId);

                // Check if any rows were affected (bid was deleted)
                if (rowsAffected == 0)
                {
                    return NotFound("No bid found with the specified ID."); // Return 404 if no bid was found
                }

                return Ok("Bid deleted successfully."); // Return success message
            }
            catch (Exception ex)
            {
                // Handle any unexpected errors
                return StatusCode(500, $"An error occurred while deleting the bid: {ex.Message}");
            }
        }

        



    }
}