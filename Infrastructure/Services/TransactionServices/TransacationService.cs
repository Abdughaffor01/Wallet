using System.Net;
using AutoMapper;
using Domain;
using Domain.DTOs;
using Domain.DTOs.TransactionDTOs;
using Domain.Entities;
using Domain.Enums;
using Domain.Wrappers;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.TransactionServices;
public class TransacationService:ITransactionService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public TransacationService(DataContext context,IMapper mapper)
    {
        _context = context;
        _mapper = mapper;   
    }

    public async Task<Response<GetTransactionDto>> AddTransactionAsync(AddTransactionDto model)
    {
        try
        {
            var from = await _context.Owners.FirstOrDefaultAsync(o => o.PhoneNumber =="+992"+ model.From);
            var to = await _context.Owners.FirstOrDefaultAsync(o => o.PhoneNumber == "+992" + model.To);
            if (from == null && to == null) return new Response<GetTransactionDto>(HttpStatusCode.NotFound,"owner not found");
            if (from.Status == StatusOwner.Unidentified) return new Response<GetTransactionDto>(HttpStatusCode.BadRequest, "please identify");
            if (from!.Balance < model.Amount) return new Response<GetTransactionDto>(HttpStatusCode.BadRequest, "insufficient balance");
            if (model.Type == TransactionType.TransferToCard) {
                var card = to.Cards.FirstOrDefault(c => c.Number == model.To);
                if (card == null) return new Response<GetTransactionDto>(HttpStatusCode.NotFound,"not found card");
                card.Balance += model.Amount;
            }
            else to!.Balance += model.Amount;
            from.Balance -= model.Amount;

            var transaction = new Transaction() {
                From = from.PhoneNumber,
                Amount = model.Amount,
                Description = model.Description,
                TransferAt = DateTime.UtcNow,
                StatusTransfer = StatusTransfer.Done,
                Type= model.Type, 
            };
            if(model.Type == TransactionType.TransferToCard) transaction.To = model.To;
            else transaction.To = to.PhoneNumber;

            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
            return new Response<GetTransactionDto>(HttpStatusCode.OK,"Successfuly transfered amount");
        }
        catch (Exception ex)
        {
            return new Response<GetTransactionDto>(HttpStatusCode.InternalServerError,ex.Message);
        }
    }

    public async Task<Response<List<GetTransitionByNumber>>> GetTransactionsAsync(int phoneNumber)
    {
        var transaction = await _context.Transactions
            .Where(t => t.From == "+992" + phoneNumber || t.To == "+992" + phoneNumber).OrderByDescending(t=>t.TransferAt).ToListAsync();
        var mapped = new List<GetTransitionByNumber>();
        foreach (var i in transaction)
        {
            var trn = new GetTransitionByNumber();
            if (i.From == "+992" + phoneNumber)
            {
                trn.Amount = i.Amount * (-1);
                trn.From = i.To;
            }
            else {
                trn.Amount = i.Amount; 
                trn.From=i.From;
            }
            trn.TransferAt = i.TransferAt;
            trn.Description = i.Description;
            trn.Type = i.Type.ToString();
            mapped.Add(trn);
        }
        return new Response<List<GetTransitionByNumber>>(mapped);
    }
}
