using Domain;
using Domain.DTOs;
using Domain.Wrappers;

namespace Infrastructure.Services;
public interface IOperationService
{
    Task<Response<string>> Payment(PaymentDto model);
    Task<Response<string>> WithDraw(WithDrawDto model);
}
