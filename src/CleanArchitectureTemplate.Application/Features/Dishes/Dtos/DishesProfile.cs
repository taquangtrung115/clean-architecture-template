using CleanArchitectureTemplate.Application.Features.Dishes.Commands.CreateDish;
using CleanArchitectureTemplate.Domain.Entities;
using Mapster;

namespace CleanArchitectureTemplate.Application.Features.Dishes.Dtos;

public class DishesProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Dish, DishDto>();
        config.NewConfig<CreateDishCommand, Dish>();
    }
}