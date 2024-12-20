using AutoMapper;
using FlightManager.Data.Entities;
using FlightManager.Shared.Dtos;
<<<<<<< HEAD
using FlightManagerMVC.ViewModels;
=======
>>>>>>> 45cb778fd5b1c6987a04924e6b5a27775830385c

namespace FlightManagerMVC
{
    internal class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<Booking, BookingDto>().ReverseMap();
<<<<<<< HEAD
            CreateMap<BookingDto, BookingEditVM>().ReverseMap();
            CreateMap<BookingDto, BookingDetailsVM>().ReverseMap();


            CreateMap<Flight, FlightDto>().ReverseMap();
            CreateMap<FlightDto, FlightEditVM>().ReverseMap();
            CreateMap<FlightDto, FlightDetailsVM>().ReverseMap();


            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<UserDto, UserEditVM>().ReverseMap();
            CreateMap<UserDto, UserDetailsVM>().ReverseMap();

=======
            

            CreateMap<Flight, FlightDto>().ReverseMap();
            

            CreateMap<User, UserDto>().ReverseMap();
           
>>>>>>> 45cb778fd5b1c6987a04924e6b5a27775830385c

        }
    }
}
