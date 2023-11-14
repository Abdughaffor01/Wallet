using Domain;
using Domain.DTOs;
using Domain.DTOs.IdentifiedDTOs;
using Domain.Wrappers;

namespace Infrastructure.Services;
public interface IIdentifiedService
{
    Task<Response<string>> AddIdentifiedToOwner(int phoneNumber);
    Task<Response<List<GetIdentifiedDto>>> GetUnIdentified();
}
