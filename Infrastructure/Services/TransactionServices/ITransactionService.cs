using Domain;
using Domain.DTOs;
using Domain.DTOs.TransactionDTOs;
using Domain.Wrappers;

namespace Infrastructure.Services;
public interface ITransactionService
{
    Task<Response<List<GetTransitionByNumber>>> GetTransactionsAsync(int phoneNumber);
    Task<Response<GetTransactionDto>> AddTransactionAsync(AddTransactionDto model);
}
