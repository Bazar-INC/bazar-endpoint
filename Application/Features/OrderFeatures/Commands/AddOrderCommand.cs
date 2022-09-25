using Application.Features.AuthFeatures.Services;
using Application.Features.OrderFeatures.Dtos;
using AutoMapper.Configuration.Annotations;
using Infrastructure.UnitOfWork.Abstract;
using MediatR;

namespace Application.Features.OrderFeatures.Commands;

public class AddOrderCommand : IRequest
{
    public ICollection<OrderDto> Orders { get; set; } = new List<OrderDto>();
    public string? Phone { get; set; }
}

public class AddOrderHandler : IRequestHandler<AddOrderCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public AddOrderHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(AddOrderCommand request, CancellationToken cancellationToken)
    {
        var productsIds = request.Orders.Select(s => s.ProductId);
        var productsTuple = request.Orders.Select(s => (s.ProductId, s.Count));

        var products = _unitOfWork.Products.Get().AsEnumerable().Where(p => productsIds.Contains(p.Id)).Select(s =>
        new Tuple<Guid, decimal>(s.Id, s.Price));

        decimal sum = 0;

        foreach (var pr in productsTuple)
        {
            var productPrice = products.FirstOrDefault(f => f.Item1 == pr.ProductId);
            var productCount = pr.Count;

            if(productPrice == default)
            {
                continue;
            }
            sum += productCount * productPrice.Item2;
        }

        // trash code TODO: remove
        var orderNumber = generateOrderNumber();
        var orderDate = generateOrderDateDelivery();

        await SmsSenderService.SendMessageAsync($"Вітаємо!\r\n" +
            $"Ми обробляємо Ваше замовлення #{orderNumber}\r\n" +
            $"Замовлення буде готовим {orderDate}\r\n" +
            $"Сума до оплати: {sum} грн", request.Phone ?? "");

        return await Unit.Task;
    }

    private string generateOrderNumber()
    {
        var orderNumberParts = new List<string>();
        for (int i = 0; i < 3; i++)
        {
            orderNumberParts.Add(CodeGeneratorService.GenerateCode());
        }
        return string.Join("", orderNumberParts);
    }

    private string generateOrderDateDelivery()
    {
        var rnd = new Random();
        var rndDate = DateTime.Now.AddDays(rnd.Next(3));
        return rndDate.ToShortDateString();
    }
}