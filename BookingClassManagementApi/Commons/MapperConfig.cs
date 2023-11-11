using AutoMapper;
using BookingClassManagementApi.Models;
using BookingClassManagementApi.ViewModels;

namespace BookingClassManagementApi.Commons
{
    public class MapperConfig:Profile
    {
        public MapperConfig()
        {
            CreateMap<User, UserVM>().ReverseMap();
            CreateMap<Package, PackageVM>().ReverseMap();
            CreateMap<Country, CountryVM>().ReverseMap();
            CreateMap<Class, ClassVM>().ReverseMap();
            CreateMap<PurchasePackage, PurchasePackageVM>().ReverseMap();
            CreateMap<Booking, BookingVM>().ReverseMap();
            CreateMap<Schdule, SchduleVM>().ReverseMap();
        }
    }
}
