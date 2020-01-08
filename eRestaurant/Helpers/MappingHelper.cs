using AutoMapper;
using SharedDto = eRestaurant.Shared.DTO;
using eRestaurant.API.Entities;
using eRestaurant.API.Helpers;
using System;

namespace eRestaurant.API.Mapping
{
    public class MappingHelper : Profile
    {
        public MappingHelper()
        {
            // MenuItem

            CreateMap<Dish, SharedDto.MenuItem>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => $"{src.Price:0.00}"))
                .ForMember(dest => dest.Portion, opt => opt.MapFrom(src => src.PortionSize))
                .ForMember(dest => dest.CookingTime, opt => opt.MapFrom(src => src.CookingTime.ToString()));
            CreateMap<UnitOfMeasurement, SharedDto.MenuItem>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Name, opt => opt.Ignore())
                .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.Name));
            CreateMap<DishType, SharedDto.MenuItem>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Name, opt => opt.Ignore())
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Name));
            CreateMap<DishImage, SharedDto.MenuItem>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => Convert.ToBase64String(src.Image)));

            // Dish DTO

            CreateMap<SharedDto.Dish, Dish>()
                .ForMember(dest => dest.CookingTime, opt => opt.MapFrom(src => (src.CookingTime.HasValue) ? TimeSpan.FromMinutes(src.CookingTime.Value) : (TimeSpan?)null))
                .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => int.Parse(src.TypeId)))
                .ForMember(dest => dest.UnitId, opt => opt.MapFrom(src => int.Parse(src.UnitId)));
            CreateMap<Dish, SharedDto.Dish>()
                .ForMember(dest => dest.CookingTime, opt => opt.MapFrom(src => (src.CookingTime.HasValue) ? src.CookingTime.Value.TotalMinutes : (double?)null))
                .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => src.TypeId.ToString()))
                .ForMember(dest => dest.UnitId, opt => opt.MapFrom(src => src.UnitId.ToString()));
        }
    }
}
