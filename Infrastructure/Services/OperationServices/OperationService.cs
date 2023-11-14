using Domain;
using Domain.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Domain.Entities;
using Domain.Enums;
using Domain.Wrappers;
using Infrastructure.Data;

namespace Infrastructure.Services.Operation;
public class OperationService:IOperationService
{
    private readonly DataContext _context;
    public OperationService(DataContext context) => _context = context;

    public async Task<Response<string>> Payment(PaymentDto model)
    {
        if (model.Type == TransactionType.TransferToCard)
        {
            var card = await _context.Cards.FirstOrDefaultAsync(c => c.Number == model.Number);
            if (card == null) return new Response<string>(HttpStatusCode.NotFound);
            card.Balance += model.Amount;
        }
        else if (model.Type == TransactionType.TransferToWallet)
        {
            var owner = await _context.Owners.FirstOrDefaultAsync(o => o.PhoneNumber == "+992" + model.Number);
            if (owner == null) return new Response<string>(HttpStatusCode.NotFound);
            owner.Balance += model.Amount;
            model.Number="+992"+model.Number;
        }
        var trk = new Transaction()
        {
            From = "11111",
            To = $"{model.Number}",
            Amount = model.Amount,
            Description = $"From terminal 111111",
            TransferAt = DateTime.UtcNow,
            StatusTransfer = Domain.Enums.StatusTransfer.Done,
            Type = model.Type,
        };
        await _context.Transactions.AddAsync(trk);
        await _context.SaveChangesAsync();
        return new Response<string>("Successfuly payment");
    }
    public async Task<Response<string>> WithDraw(WithDrawDto model) {
        var card = await _context.Cards.FirstOrDefaultAsync(c=>c.Number==model.CardNumber);
        if (card == null) return new Response<string>(HttpStatusCode.NotFound,"no card");
        if (card.SecurityCode != model.Password) return new Response<string>(HttpStatusCode.BadRequest,"error password");
        if (card.Balance < model.Amount) return new Response<string>(HttpStatusCode.BadRequest,"no balance");
        card.Balance -= model.Amount;
        var trk = new Transaction()
        {
            From = $"{model.CardNumber}",
            To = "00000",
            Amount = model.Amount,
            Description = $"From bankomat 00000",
            TransferAt = DateTime.UtcNow,
            StatusTransfer = Domain.Enums.StatusTransfer.Done,
            Type = TransactionType.WithDraw,
        };
        await _context.Transactions.AddAsync(trk);
        await _context.SaveChangesAsync();
        return new Response<string>("Successfuly withdraw");
    }
}
