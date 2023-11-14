using Domain;
using Domain.DTOs;
using Domain.DTOs.IdentifiedDTOs;
using Domain.Wrappers;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
namespace WebApi.Controllers;
[Route("[controller]")]
public class IdentifiedController:ControllerBase
{
    private readonly IIdentifiedService _service;
    public IdentifiedController(IIdentifiedService service)=>_service = service;


    [HttpGet("GetUnIdentified")]
    public async Task<Response<List<GetIdentifiedDto>>> GetUnIdentified()
    {
        return await _service.GetUnIdentified();
    }

    [HttpPut("AddIdentifiedToOwner")]
    public async Task<Response<string>> AddIdentifiedToOwner(int phoneNumber)
    {
        return await _service.AddIdentifiedToOwner(phoneNumber);
    }
}
