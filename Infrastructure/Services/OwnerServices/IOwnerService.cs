using Domain;
using Domain.DTOs;
using Domain.DTOs.OwnerDTOs;
using Domain.Wrappers;

namespace Infrastructure.Services;
public interface IOwnerService
{
    Task<Response<List<GetOwnerDto>>> GetOwnersAsync();
    Task<Response<GetOwnerWithInfo>> GetOwnerByNumberAsync(int id);
    Task<Response<GetOwnerDto>> AddOwnerAsync(AddOwnerDto model);
    Task<Response<GetOwnerDto>> UpdateOwnerAsync(UpdateOwnerDto model);
    Task<Response<string>> DeleteOwnerAsync(int id);
}
