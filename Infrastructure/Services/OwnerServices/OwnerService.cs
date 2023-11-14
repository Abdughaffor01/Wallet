using System.Net;
using AutoMapper;
using Domain;
using Domain.DTOs;
using Domain.DTOs.OwnerDTOs;
using Domain.Entities;
using Domain.Wrappers;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.OwnerServices;
public class OwnerService : IOwnerService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public OwnerService(DataContext context,IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<Response<GetOwnerDto>> AddOwnerAsync(AddOwnerDto model)
    {
        try
        {
            var find = await _context.Owners.FirstOrDefaultAsync(o => o.PhoneNumber== "+992" + model.PhoneNumber);
            var owner = new Owner() {
                PhoneNumber = "+992" + model.PhoneNumber,
                Balance = 0,
                Limit = 1360,
                CreatedAt = DateTime.UtcNow,
                Status = Domain.Enums.StatusOwner.Unidentified
            };
            if (find != null) return new Response<GetOwnerDto>(HttpStatusCode.BadRequest, "There is an account using this number");
            await _context.Owners.AddAsync(owner);
            await _context.SaveChangesAsync();
            var mapped=_mapper.Map<GetOwnerDto>(owner);
            return new Response<GetOwnerDto>(mapped);
        }
        catch (Exception ex)
        {
            return new Response<GetOwnerDto>(HttpStatusCode.InternalServerError,ex.Message);
        }
    }

    public async Task<Response<string>> DeleteOwnerAsync(int id)
    {
        try
        {
            var owner = await _context.Owners.FindAsync(id);
            if(owner==null)return new Response<string>(HttpStatusCode.NotFound);
            _context.Owners.Remove(owner);
            await _context.SaveChangesAsync();
            return new Response<string>("Successfuly deleted owner");
        }
        catch (Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError,ex.Message);
        }
    }

    public async Task<Response<GetOwnerWithInfo>> GetOwnerByNumberAsync(int id)
    {
        try
        {
            var owner = await _context.Owners.Select(o => new GetOwnerWithInfo()
            {
                PhoneNumber = o.PhoneNumber,
                Balance = o.Balance,
                Cards = o.Cards,
                Limit=o.Limit,
                CreatedAt = o.CreatedAt,
                Status = o.Status.ToString()
            }).FirstOrDefaultAsync(o => o.PhoneNumber == "+992"+id);
            if (owner == null) return new Response<GetOwnerWithInfo>(HttpStatusCode.NotFound);
            return new Response<GetOwnerWithInfo>(owner);
        }
        catch (Exception ex)
        {
            return new Response<GetOwnerWithInfo>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<Response<List<GetOwnerDto>>> GetOwnersAsync()
    {

        var owners = await _context.Owners.ToListAsync();
        var mapped = _mapper.Map<List<GetOwnerDto>>(owners);
        return new Response<List<GetOwnerDto>>(mapped);
    }

    public async Task<Response<GetOwnerDto>> UpdateOwnerAsync(UpdateOwnerDto model)
    {
        var owner=await _context.Owners.FirstOrDefaultAsync(o=>o.PhoneNumber =="+992" + model.PhoneNumber);
        if (owner == null) return new Response<GetOwnerDto>(HttpStatusCode.NotFound);
        if(model.FirstName!=null) owner.FirstName = model.FirstName;
        if(model.LastName !=null) owner.LastName = model.LastName;
        owner.Limit = 10000;
        await _context.SaveChangesAsync();
        var mapped = new GetOwnerDto() {
            FirstName=owner.FirstName,
            LastName=owner.LastName,
            Limit=owner.Limit,
            PhoneNumber = owner.PhoneNumber,
            Balance = owner.Balance,
            CreatedAt = owner.CreatedAt,
            Status = owner.Status.ToString()
        };
        return new Response<GetOwnerDto>(mapped);
    }
}
