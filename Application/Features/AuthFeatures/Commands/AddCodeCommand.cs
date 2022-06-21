using AutoMapper;
using Core.Entities;
using Infrastructure.UnitOfWork.Abstract;
using MediatR;

namespace Application.Features.AuthFeatures.Commands;

public record AddCodeCommand(string Phone) : IRequest;

public class AddCodeHandler : IRequestHandler<AddCodeCommand>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public AddCodeHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<Unit> Handle(AddCodeCommand request, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<CodeEntity>(request);

        var code = "1234";//var code = CodeGeneratorService.GenerateCode(); 

        entity.Code = code;

        //await SmsSenderService.SendMessageAsync("1234", request.Phone);
        await unitOfWork.Codes.InsertAsync(entity);

        return default;
    }
}