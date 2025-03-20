using AutoMapper;
using Testingcollections.Dto;
using Testingcollections.Models;



namespace Testingcollections.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Seller, SellerDto>();
            CreateMap<SellerDto, Seller>();
            CreateMap<Advert, AdvertDto>();
            CreateMap<AdvertDto, Advert>();
            CreateMap<State, StateDto>();
            CreateMap<StateDto, State>();
            CreateMap<Vertical, VerticalDto>();
            CreateMap<VerticalDto, Vertical>();
        }
    }
}