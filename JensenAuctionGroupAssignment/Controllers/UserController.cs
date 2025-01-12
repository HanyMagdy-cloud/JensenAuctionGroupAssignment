using AutoMapper;
using JensenAuctionGroupAssignment.Dto;
using JensenAuctionGroupAssignment.Entites;
using JensenAuctionGroupAssignment.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Generators;

namespace JensenAuctionGroupAssignment.Controllers
{
    [Authorize] // Ensure the user is logged in]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository; // Repository for user operations
        private readonly IMapper _mapper; // AutoMapper instance
        private readonly IJensenAuction _dbContext; // Add _dbContext field


        public UserController(IUserRepository userRepository, IMapper mapper, IJensenAuction dbContext)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _dbContext = dbContext;
        }

        [AllowAnonymous]
        // Create a new user
        [HttpPost("register_New_User")]
        public IActionResult Register(CreateUserDTO createUserDTO)
        {
            var user = _mapper.Map<User>(createUserDTO);

            var userId = _userRepository.CreateUser(user.Username, user.Password); // Get the correct UserId

            // Set the UserId directly in the mapped user object
            user.UserID = userId;
            var userDTO = _mapper.Map<UserDTO>(user); // Map after setting the ID

            return CreatedAtAction(nameof(GetUser), new { username = user.Username }, userDTO); // Use CreatedAtAction for 201
        }

        // Get a user by username
        [HttpGet("{username}/Get_a_user_by_username")]
        public IActionResult GetUser(string username)
        {
            // Retrieve the user from the repository
            var user = _userRepository.GetUserByUsername(username);

            if (user == null)
            {
                // Return 404 if the user is not found
                return NotFound("User not found.");
            }

            // Map the user to a DTO
            var userDTO = _mapper.Map<UserDTO>(user);
            return Ok(userDTO);
        }




    }




}
