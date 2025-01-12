using JensenAuctionGroupAssignment.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JensenAuctionGroupAssignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IUserRepository _userRepository; // Repository for user operations
        private readonly IJensenAuction _dbContext; // Add _dbContext field

        public TestController(IUserRepository userRepository, IJensenAuction dbContext)
        {
            _userRepository = userRepository;
            _dbContext = dbContext;
        }

        [AllowAnonymous]
        [HttpGet("testdb")]
        public IActionResult TestDatabase()
        {
            try
            {
                using (var connection = _dbContext.GetConnection())
                {
                    connection.Open();
                    return Ok("Database connection successful!");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Database connection failed: {ex.Message}");
            }
        }
    }
}
