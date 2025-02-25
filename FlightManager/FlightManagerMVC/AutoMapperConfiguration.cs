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
            CreateMap<Booking, BookingDto>()
                .ForMember(dest => dest.Flight, opt => opt.MapFrom(src => src.Flight))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));
            CreateMap<BookingDto, Booking>();

            CreateMap<BookingDto, BookingEditVM>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));
            CreateMap<BookingEditVM, BookingDto>();

            CreateMap<BookingDto, BookingDetailsVM>()
                .ForMember(dest => dest.Flight, opt => opt.MapFrom(src => src.Flight))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));
            CreateMap<BookingDetailsVM, BookingDto>();

            CreateMap<Flight, FlightDto>().ReverseMap();
            CreateMap<FlightDto, FlightEditVM>().ReverseMap();
            CreateMap<FlightDto, FlightDetailsVM>().ReverseMap();

            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<UserDto, UserEditVM>().ReverseMap();
            CreateMap<UserDto, UserDetailsVM>().ReverseMap();

            CreateMap<LoginVM, LoginDto>().ReverseMap();
        }
    }
}
