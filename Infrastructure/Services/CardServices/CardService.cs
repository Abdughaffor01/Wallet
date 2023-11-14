using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Domain.DTOs.CardDTOs;
using Domain.Entities;
using Domain.Wrappers;
using Infrastructure.Data;

namespace Infrastructure.Services.CardServices;
public class CardService:ICardService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public CardService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<GetCardDto>> AddCartToOwner(AddCartDto model)
    {
        try
        {
            var r=new Random();
            var owner = await _context.Owners
                .FirstOrDefaultAsync(o => o.PhoneNumber == "+992"+model.PhoneNumber);
            if (owner == null) return new Response<GetCardDto>(HttpStatusCode.NotFound,"not found owner");
            var card = new Card()
            {
                Balance = 0,
                Limit = 1500,
                Name = "Alif",
                Number = $"5058-2702-{r.Next(1000, 9999)}-{r.Next(1000, 9999)}",
                OwnerPhoneNumber = owner.PhoneNumber,
                SecurityCode = $"{r.Next(1000, 9999)}",
                TermKart = 4
            };
            await _context.Cards.AddAsync(card);
            await _context.SaveChangesAsync();
            var mapped = _mapper.Map<GetCardDto>(card);
            return new Response<GetCardDto>(mapped);
        }
        catch (Exception ex)
        {
            return new Response<GetCardDto>(HttpStatusCode.InternalServerError,ex.Message);
        }
    }

    public async Task<Response<List<Card>>> GetCarts()
    {
        var card = await _context.Cards.ToListAsync();
        return new Response<List<Card>>(card);
    }

    public Task<Response<GetCardDto>> GetCartWithNumber(int CartNumber)
    {
        throw new NotImplementedException();
    }
}
