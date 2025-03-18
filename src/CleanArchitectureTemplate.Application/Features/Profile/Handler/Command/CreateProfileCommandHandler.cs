using CleanArchitectureTemplate.Application.DTO.BaseDTO;
using CleanArchitectureTemplate.Application.Features.Profile.Request.Command;
using CleanArchitectureTemplate.Domain.Interfaces;
using MapsterMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureTemplate.Domain.Entities;

namespace CleanArchitectureTemplate.Application.Features.Profile.Handler.Command;
public class CreateProfileCommandHandler(IUnitOfWork unitOfWork
            , IMapper mapper) : IRequestHandler<CreateProfileCommand, BaseCommandResponse>
{
    public async Task<BaseCommandResponse> Handle(CreateProfileCommand request, CancellationToken cancellationToken)
    {
        var profile = mapper.Map<CleanArchitectureTemplate.Domain.Entities.Profile>(request.ProfileCreate);

        profile = await unitOfWork.ProfileRepository.AddAsync(profile);

        return new BaseCommandResponse(profile.Id, true);
    }
}
