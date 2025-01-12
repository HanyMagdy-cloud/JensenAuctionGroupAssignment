namespace JensenAuctionGroupAssignment.Interfaces
{
    public interface IAuthService
    {
        string Authenticate(string username, string password); // Returns JWT token

    }
}
