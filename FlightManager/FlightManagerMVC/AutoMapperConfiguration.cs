using AutoMapper;
using FlightManager.Data.Entities;
using FlightManager.Shared.Dtos;

using FlightManagerMVC.ViewModels;


namespace FlightManagerMVC
{
    internal class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<Booking, BookingDto>().ReverseMap();

            CreateMap<BookingDto, BookingEditVM>().ReverseMap();
            CreateMap<BookingDto, BookingDetailsVM>().ReverseMap();


            CreateMap<Flight, FlightDto>().ReverseMap();
            CreateMap<FlightDto, FlightEditVM>().ReverseMap();
            CreateMap<FlightDto, FlightDetailsVM>().ReverseMap();


            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<UserDto, UserEditVM>().ReverseMap();
            CreateMap<UserDto, UserDetailsVM>().ReverseMap();

            

            CreateMap<Flight, FlightDto>().ReverseMap();
            

            CreateMap<User, UserDto>().ReverseMap();
           

        }
    }
}
