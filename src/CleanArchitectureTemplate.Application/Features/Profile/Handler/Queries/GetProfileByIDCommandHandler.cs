using CleanArchitectureTemplate.Application.DTO.BaseDTO;
using CleanArchitectureTemplate.Application.DTO.Profile;
using CleanArchitectureTemplate.Application.Features.Profile.Request.Queries;
using CleanArchitectureTemplate.Domain.Entities;
using CleanArchitectureTemplate.Domain.Exceptions;
using CleanArchitectureTemplate.Domain.Interfaces;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureTemplate.Application.Features.Profile.Handler.Queries;

public class GetProfileByIDCommandHandler(ILogger<GetProfileByIDQueryRequest> logger
    , IUnitOfWork unitOfWork
    , IMapper mapper
    , UserManager<ApplicationUser> userManager
    ) : IRequestHandler<GetProfileByIDQueryRequest, BaseCommandResponse<Guid>>
{
    public async Task<BaseCommandResponse<Guid>> Handle(GetProfileByIDQueryRequest request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting profile {ProfileID}", request.Id);
        var userByID = userManager.FindByIdAsync(request.Id.ToString());
        var res = new BaseCommandResponse<Guid>();
        if (userByID.Result != null && string.IsNullOrEmpty(userByID.Result.Id))
        {
            res.Errors = new List<string> { "User not found" };
            return res;
        }
        var user = userByID.Result;
        var lstProfile = await unitOfWork.ProfileRepository.GetAllAsync();

        var profile = lstProfile.FirstOrDefault(s => s.UserID == Guid.Parse(user.Id));

        var profileDTO = mapper.Map<ProfileDTO>(profile);
        if (profileDTO != null)
        {
            res.Success = true;
            res.Datas = new List<object> { profileDTO };
        }
        else
        {
            res.Errors = new List<string> { "Profile not found" };
        }
        return res;
    }
}
