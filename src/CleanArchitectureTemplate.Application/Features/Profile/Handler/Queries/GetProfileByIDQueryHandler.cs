using CleanArchitectureTemplate.Application.DTO.Profile;
using CleanArchitectureTemplate.Application.Features.Profile.Request.Queries;
using CleanArchitectureTemplate.Domain.Exceptions;
using CleanArchitectureTemplate.Domain.Interfaces;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureTemplate.Application.Features.Profile.Handler.Queries;

public class GetProfileByIDQueryHandler(ILogger<GetProfileByIDQuery> logger
    , IUnitOfWork unitOfWork
    , IMapper mapper
    ) : IRequestHandler<GetProfileByIDQuery, ProfileDTO>
{
    public async Task<ProfileDTO> Handle(GetProfileByIDQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting profile {ProfileID}", request.Id);
        var profile = await unitOfWork.ProfileRepository.GetByIdAsync(request.Id);
        var profileDTO = mapper.Map<ProfileDTO>(profile)
                         ?? throw new NotFoundException(nameof(CleanArchitectureTemplate.Domain.Entities.Profile), request.Id.ToString());
        return profileDTO;
    }
}
