using Dapper;
using JensenAuctionGroupAssignment.Dto;
using JensenAuctionGroupAssignment.Entites;
using JensenAuctionGroupAssignment.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace JensenAuctionGroupAssignment.Controllers
{
    [Authorize] // Ensure the user is logged in
    [Route("api/[controller]")]
    [ApiController]
    public class AuctionController : ControllerBase
    {
        private readonly IAuctionRepository _auctionRepository;

        private readonly IJensenAuction _dbContext; // Add _dbContext field

        private readonly IBidRepository _bidRepository; // Add IBidRepository for bid operations



        public AuctionController(IAuctionRepository auctionRepository, IJensenAuction dbContext, IBidRepository bidRepository)
        {
            _auctionRepository = auctionRepository;
            _dbContext = dbContext;
            _bidRepository = bidRepository; // Add IBidRepository

        }

        [HttpPost("Create_Auction")]
        public IActionResult CreateAuction(CreateAuctionDTO auctionDTO)
        {
            var auctionId = _auctionRepository.CreateAuction(auctionDTO);
            return Ok(new { AuctionID = auctionId });
        }

        [HttpGet("{auctionId}")]
        public IActionResult GetAuctionById(int auctionId)
        {
            var auction = _auctionRepository.GetAuctionById(auctionId);
            if (auction == null)
            {
                return NotFound($"Auction with id {auctionId} is not found");
            }
            return Ok(auction);
        }

        [AllowAnonymous]
        [HttpGet("Get_All_Auctions")]
        public IActionResult GetAllAuctions()
        {
            var auctions = _auctionRepository.GetAllAuctions();
            return Ok(auctions);
        }


        [HttpDelete("{auctionId}/Delete_Auction_ByUserId/{userId}")]
        public IActionResult DeleteAuctionByUserId(int auctionId, int userId)
        {
            var rowsAffected = _auctionRepository.DeleteAuctionByUserId(auctionId, userId);

            if (rowsAffected == 0)
            {
                return NotFound("No auction found for the given user ID and auction ID.");
            }

            return Ok("Auction deleted successfully.");
        }

        
        
        [AllowAnonymous]
        [HttpGet("search")]
        public IActionResult SearchAuctions([FromQuery] string searchTerm)
        {
            // Check if the search term is provided
            if (string.IsNullOrEmpty(searchTerm))
            {
                return BadRequest("Search term is required.");
            }

            // Call the repository method to search auctions
            var auctions = _auctionRepository.SearchAuctions(searchTerm);

            // If no auctions match the search term, return a 404 response
            if (auctions == null || auctions.Count == 0)
            {
                return NotFound("No auctions match the search term.");
            }

            // Return the matching auctions
            return Ok(auctions);
        }

        [HttpPut("{auctionId}/Update_Auction")]
        public IActionResult UpdateAuction(int auctionId, int userId, [FromQuery] UpdateAuctionDTO auctionDTO)
        {
            if (auctionDTO == null)
            {
                return BadRequest("The auctionDTO field is required.");
            }

            try
            {
                // Call the repository method to update the auction
                var rowsAffected = _auctionRepository.UpdateAuctionByUserId(auctionId, userId, auctionDTO);

                if (rowsAffected == 0)
                {
                    return NotFound("No auction found for the given user ID and auction ID.");
                }

                return Ok("Auction updated successfully.");
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





    }
}
