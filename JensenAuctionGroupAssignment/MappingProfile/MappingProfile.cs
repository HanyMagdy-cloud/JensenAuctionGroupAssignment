using AutoMapper;
using JensenAuctionGroupAssignment.Dto;
using JensenAuctionGroupAssignment.Entites;

namespace JensenAuctionGroupAssignment.MappingProfile
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            // User mappings
            CreateMap<User, UserDTO>(); // Maps User -> UserDTO
            CreateMap<CreateUserDTO, User>(); // Maps CreateUserDTO -> User

            // Auction mappings
            CreateMap<Auction, AuctionDTO>(); // Maps Auction -> AuctionDTO
            CreateMap<CreateAuctionDTO, Auction>(); // Maps CreateAuctionDTO -> Auction

            // Bid mappings
            CreateMap<Bid, BidDTO>(); // Maps Bid -> BidDTO
            CreateMap<PlaceBidDTO, Bid>(); // Maps PlaceBidDTO -> Bid
        }
    }
}
