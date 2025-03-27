using CleanArchitectureTemplate.Application.DTO.BaseDTO;
using CleanArchitectureTemplate.Application.Features.AdditionImgUrl.Request.Command.DeleteAddtionImgUrl;
using CleanArchitectureTemplate.Domain.Interfaces;
using MapsterMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureTemplate.Application.Features.AdditionImgUrl.Handler.Command.DeleteAddtionImgUrl;

public class DeleteAddtionImgUrlCommandHandler(IUnitOfWork unitOfWork
            , IMapper mapper) : IRequestHandler<DeleteAddtionImgUrlCommandRequest, BaseCommandResponse<Guid>>
{
    public async Task<BaseCommandResponse<Guid>> Handle(DeleteAddtionImgUrlCommandRequest request, CancellationToken cancellationToken)
    {
        unitOfWork.AdditionImgUrlReponsitory.RemoveRange(request.ListID);
        await unitOfWork.SaveChangeAsync();

        return new BaseCommandResponse<Guid>
        {
            ID = Guid.NewGuid(),
            Success = true,
            Message = "Deletion successful",
            Errors = new List<string>()
        };
    }
}
