
namespace Application.Features.OrderFeatures.Dtos;

public record OrderDto
{
    public Guid ProductId { get; set; }
    public int Count { get; set; }
}
