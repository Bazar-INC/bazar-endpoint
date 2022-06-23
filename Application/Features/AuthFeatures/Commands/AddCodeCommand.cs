using AutoMapper;
using Core.Entities;
using Infrastructure.UnitOfWork.Abstract;
using MediatR;

namespace Application.Features.AuthFeatures.Commands;

public record AddCodeCommand(string PhoneNumber) : IRequest;

public class AddCodeHandler : IRequestHandler<AddCodeCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AddCodeHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(AddCodeCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<CodeEntity>(request);

        var code = "1234"; // TODO: delete line
        //var code = CodeGeneratorService.GenerateCode();  // TODO: uncomment line

        entity.Code = code;

        //await SmsSenderService.SendMessageAsync("1234", request.Phone); // TODO: uncomment line
        await _unitOfWork.Codes.InsertAsync(entity);

        await _unitOfWork.SaveChangesAsync();

        return default;
    }
}