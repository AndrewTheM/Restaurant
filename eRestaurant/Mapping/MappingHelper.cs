using AutoMapper;
using eRestaurant.DTO;
using eRestaurant.Entities;
using eRestaurant.Helpers;

namespace eRestaurant.Mapping
{
    public class MappingHelper : Profile
    {
        public MappingHelper()
        {
            // MenuItemResponse

            CreateMap<Dish, MenuItem>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => $"{src.Price:0.00}"))
                .ForMember(dest => dest.Portion, opt => opt.MapFrom(src => src.PortionSize))
                .ForMember(dest => dest.CookingTime, opt => opt.MapFrom(src => src.CookingTime.ToString()));
            CreateMap<UnitOfMeasurement, MenuItem>()
                .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.Name));
            CreateMap<DishType, MenuItem>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Name));
            CreateMap<DishImage, MenuItem>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image));

            // PaginationHelper

            CreateMap(typeof(PagedList<>), typeof(PaginationHelper));
        }
    }
}
