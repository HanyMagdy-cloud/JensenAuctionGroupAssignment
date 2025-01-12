namespace JensenAuctionGroupAssignment.Dto
{
    // Data Transfer Object for creating a new user
    public class CreateUserDTO
    {
        public string Username { get; set; } // Username for the new user
        public string Password { get; set; } // Plain-text password (to be hashed)
    }
}
