using CleanArchitectureTemplate.Application.DTO.BaseDTO;
using CleanArchitectureTemplate.Application.Features.AdditionImgUrl.Request.Command.CreateOrUpdateAddtionImgUrl;
using CleanArchitectureTemplate.Domain.Interfaces;
using MapsterMapper;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.AdditionImgUrl.Handler.Command.CreateOrUpdateAddtionImgUrl;

public class AdditionImgUrlCommandHandler(IUnitOfWork unitOfWork
            , IMapper mapper) : IRequestHandler<AdditionImgUrlCommandRequest, BaseCommandResponse<Guid>>
{
    public async Task<BaseCommandResponse<Guid>> Handle(AdditionImgUrlCommandRequest request, CancellationToken cancellationToken)
    {
        if (request.AdditionImgUrlDTO == null)
        {
            return new BaseCommandResponse<Guid>
            {
                ID = Guid.Empty,
                Errors = new List<string> { "AdditionImgUrlDTO is null" },
            };
        }

        var additionImgUrl = mapper.Map<Domain.Entities.AdditionImgUrl>(request.AdditionImgUrlDTO);

        if (additionImgUrl.Id != Guid.Empty)
        {
            unitOfWork.AdditionImgUrlReponsitory.Update(additionImgUrl);
            return new BaseCommandResponse<Guid>
            {
                ID = additionImgUrl.Id,
                Success = true,
                Message = "Update AdditionImgUrl successfully processed",
            };
        }
        else
        {
            var result = unitOfWork.AdditionImgUrlReponsitory.AddAsync(additionImgUrl);
            if (result != null)
            {
                return new BaseCommandResponse<Guid>
                {
                    ID = result.Result.Id,
                    Success = true,
                    Message = "AdditionImgUrl successfully processed",
                };
            }
        }
        await unitOfWork.SaveChangeAsync<Guid>();
        return new BaseCommandResponse<Guid>
        {
            ID = Guid.Empty,
            Errors = new List<string> { "AdditionImgUrlDTO is Error" },
        };
    }

}
