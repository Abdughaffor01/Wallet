using Domain.DTOs;
using Domain;
using Domain.DTOs.CardDTOs;
using Domain.Entities;
using Domain.Wrappers;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
namespace WebApi.Controllers;
[Route("[controller]")]
public class CardController:ControllerBase
{
    private readonly ICardService _service;

    public CardController(ICardService service)=>_service = service;

    [HttpGet("GetCarts")]
    public async Task<Response<List<Card>>> GetCarts()=> await _service.GetCarts();

    [HttpPost("AddCartToOwner")]
    public async Task<Response<GetCardDto>> AddCartToOwner(AddCartDto model) { 
        return await _service.AddCartToOwner(model);
    }

}
