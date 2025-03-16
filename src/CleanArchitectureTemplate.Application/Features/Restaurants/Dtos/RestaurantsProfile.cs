using CleanArchitectureTemplate.Application.Features.Restaurants.Commands.CreateRestaurant;
using CleanArchitectureTemplate.Domain.Entities;
using Mapster;

namespace CleanArchitectureTemplate.Application.Features.Restaurants.Dtos;

public class RestaurantsProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateRestaurantCommand, Restaurant>()
            .Map(dest => dest.Address, src => new Address
        {
            City = src.City,
            PostalCode = src.PostalCode,
            Street = src.Street
        });
        
        config.NewConfig<Restaurant, RestaurantDto>()
            .Map(dest => dest.City, src => src.Address == null ? null : src.Address.City)
            .Map(dest => dest.PostalCode, src => src.Address == null ? null : src.Address.PostalCode)
            .Map(dest => dest.Street, src => src.Address == null ? null : src.Address.Street)
            .Map(dest => dest.Dishes, src => src.Dishes);
    }
}