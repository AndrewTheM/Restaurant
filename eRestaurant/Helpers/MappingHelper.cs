using AutoMapper;
using eRestaurant.DTO;
using eRestaurant.Entities;
using eRestaurant.Helpers;
using System;

namespace eRestaurant.Mapping
{
    public class MappingHelper : Profile
    {
        public MappingHelper()
        {
            // MenuItemResponse

            CreateMap<Dish, MenuItem>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => $"{src.Price:0.00}"))
                .ForMember(dest => dest.Portion, opt => opt.MapFrom(src => src.PortionSize))
                .ForMember(dest => dest.CookingTime, opt => opt.MapFrom(src => src.CookingTime.ToString()));
            CreateMap<UnitOfMeasurement, MenuItem>()
                .ForMember(dest => dest.Name, opt => opt.Ignore())
                .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.Name));
            CreateMap<DishType, MenuItem>()
                .ForMember(dest => dest.Name, opt => opt.Ignore())
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Name));
            CreateMap<DishImage, MenuItem>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => Convert.ToBase64String(src.Image)));

            // DishRequest

            CreateMap<DishRequest, Dish>()
                .ForMember(dest => dest.CookingTime, opt => opt.MapFrom(src => (src.CookingTime == null) ? (TimeSpan?)null : TimeSpan.FromMinutes(src.CookingTime.Value)))
                .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => int.Parse(src.TypeId)))
                .ForMember(dest => dest.UnitId, opt => opt.MapFrom(src => int.Parse(src.UnitId)));
        }
    }
}
