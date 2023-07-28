using Api.Dtos;
using Api.Entities;
using AutoMapper;

namespace Api.Settings
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<SignUpDto, User>();
            CreateMap<User, AccountDto>();
            CreateMap<Address, AddressDto>();
            CreateMap<CreateAddressDto, Address>();
            CreateMap<Subscription, SubscriptionDto>();
        }
    }
}
