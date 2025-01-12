namespace JensenAuctionGroupAssignment.Entites
{
    // Represents the User entity in the database
    public class User
    {
        public int UserID { get; set; } // Unique identifier for the user
        public string Username { get; set; } // Username of the user (unique)
        public string Password { get; set; } // Hashed password of the user
        public DateTime CreatedAt { get; set; } = DateTime.Now; // Date and time the user was created
    }

}
