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
            , IMapper mapper) : IRequestHandler<CreateProfileCommand, BaseCommandResponse<Guid>>
{
    public async Task<BaseCommandResponse<Guid>> Handle(CreateProfileCommand request, CancellationToken cancellationToken)
    {
        var profile = mapper.Map<CleanArchitectureTemplate.Domain.Entities.Profile>(request.ProfileCreate);

        profile = await unitOfWork.ProfileRepository.AddAsync(profile);
        await unitOfWork.SaveChangeAsync<Guid>();
        return new BaseCommandResponse<Guid>(profile.Id, true);
    }
}
