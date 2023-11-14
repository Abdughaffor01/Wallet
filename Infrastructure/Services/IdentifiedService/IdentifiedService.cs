using System.Net;
using Domain;
using Domain.DTOs;
using Domain.DTOs.IdentifiedDTOs;
using Domain.Enums;
using Domain.Wrappers;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.IdentifiedService;
public class IdentifiedService:IIdentifiedService
{
    private readonly DataContext _context;
    public IdentifiedService(DataContext context)=>_context = context;



    public async  Task<Response<string>> AddIdentifiedToOwner(int phoneNumber)
    {
        try
        {
            var owner = await _context.Owners.Where(o => o.FirstName != null & o.LastName != null)
                .FirstOrDefaultAsync(o=>o.PhoneNumber=="+992"+phoneNumber);
            if (owner == null) return new Response<string>(HttpStatusCode.NotFound);
            owner.Status = StatusOwner.Identified;
            await _context.SaveChangesAsync();
            return new Response<string>("Successfuly identified owner");

        }
        catch (Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError,ex.Message);
        }
    }

    public async Task<Response<List<GetIdentifiedDto>>> GetUnIdentified()
    {
        var owners = await _context.Owners
            .Where(o => o.FirstName != null & o.LastName != null & o.Status==StatusOwner.Unidentified).ToListAsync();
        if (owners.Count == 0) return new Response<List<GetIdentifiedDto>>(HttpStatusCode.NotFound,"no request");
        var ownersMapped=new List<GetIdentifiedDto>();
        foreach (var o in owners)
        {
            var owner = new GetIdentifiedDto()
            {
                FirstName = o.FirstName,
                LastName = o.FirstName,
                PhoneNumber = o.PhoneNumber,
                Status = o.Status.ToString(),
            };
            ownersMapped.Add(owner);
        }
        return new Response<List<GetIdentifiedDto>>(ownersMapped);
    }
}
