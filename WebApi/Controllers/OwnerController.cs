using Domain.DTOs;
using Domain;
using Domain.DTOs.OwnerDTOs;
using Domain.Wrappers;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
namespace WebApi.Controllers;
[Route("[controller]")]
public class OwnerController:ControllerBase
{
    private readonly IOwnerService _service;
    public OwnerController(IOwnerService service)=>_service = service;

    [HttpGet("GetOwnersAsync")]
    public async Task<Response<List<GetOwnerDto>>> GetOwnersAsync() {
        return await _service.GetOwnersAsync();
    }

    [HttpGet("GetOwnerAsync")]
    public async Task<Response<GetOwnerWithInfo>> GetOwnerByNumberAsync(int phoneNumber) { 
        return await _service.GetOwnerByNumberAsync(phoneNumber);
    }

    [HttpPost("AddOwnerAsync")]
    public async Task<Response<GetOwnerDto>> AddOwnerAsync(AddOwnerDto model) { 
        return await _service.AddOwnerAsync(model);
    }

    [HttpPut("UpdateOwnerAsync")]
    public async Task<Response<GetOwnerDto>> UpdateOwnerAsync(UpdateOwnerDto model) { 
        return await _service.UpdateOwnerAsync(model);
    }

    [HttpDelete("DeleteOwnerAsync")]
    public async Task<Response<string>> DeleteOwnerAsync(int phoneNumber) { 
        return await _service.DeleteOwnerAsync(phoneNumber);
    }
}
