using CleanArchitectureTemplate.Application.DTO.Profile;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureTemplate.Application.Features.Profile.Request.Queries
{
    //GetRestaurantByIdQuery(int id) : IRequest<RestaurantDto>
    public class GetProfileByIDQuery(Guid id) : IRequest<ProfileDTO>
    {
        public Guid Id { get; set; }
    }
}
