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
        }
    }
}
