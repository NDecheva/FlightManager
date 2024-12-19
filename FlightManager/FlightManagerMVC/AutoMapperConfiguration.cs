using AutoMapper;
using FlightManager.Data.Entities;
using FlightManager.Shared.Dtos;

namespace FlightManagerMVC
{
    internal class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<Booking, BookingDto>().ReverseMap();
            

            CreateMap<Flight, FlightDto>().ReverseMap();
            

            CreateMap<User, UserDto>().ReverseMap();
           

        }
    }
}
