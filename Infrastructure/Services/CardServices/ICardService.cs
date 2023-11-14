using Domain;
using Domain.DTOs;
using Domain.DTOs.CardDTOs;
using Domain.Entities;
using Domain.Wrappers;

namespace Infrastructure.Services;
public interface ICardService
{
    Task<Response<List<Card>>> GetCarts();
    Task<Response<GetCardDto>> AddCartToOwner(AddCartDto model);
    Task<Response<GetCardDto>> GetCartWithNumber(int CartNumber);
}
