using JensenAuctionGroupAssignment.Entites;

namespace JensenAuctionGroupAssignment.Interfaces
{  
    // Interface for user-related operations

    public interface IUserRepository
    {
        int CreateUser(string username, string passwordHash); // Creates a new user and returns the UserId
        User GetUserByUsername(string username); // Retrieves a user by their username
    }
}
