using Domain.DTOs;
using Domain;
using Domain.DTOs.TransactionDTOs;
using Domain.Wrappers;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
namespace WebApi.Controllers;
[Route("[controller]")]
public class TransactionController:ControllerBase
{
    private readonly ITransactionService _service;
    public TransactionController(ITransactionService service)=>_service = service;

    [HttpPost("AddTransactionAsync")]
    public async Task<Response<GetTransactionDto>> AddTransactionAsync(AddTransactionDto model) { 
        return await _service.AddTransactionAsync(model);
    }

    [HttpGet("GetTransactionsAsync")]
    public async Task<Response<List<GetTransitionByNumber>>> GetTransactionsAsync(int phoneNumber) { 
        return await _service.GetTransactionsAsync(phoneNumber);
    }
}
