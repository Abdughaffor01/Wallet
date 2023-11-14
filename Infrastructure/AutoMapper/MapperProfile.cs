using AutoMapper;
using Domain;
using Domain.DTOs;
using Domain.DTOs.CardDTOs;
using Domain.DTOs.OwnerDTOs;
using Domain.Entities;

namespace Infrastructure.AutoMapper;
public class MapperProfile:Profile
{
    public MapperProfile()
    {
        CreateMap<Owner, GetOwnerDto>()
            .ForMember(go => go.Status, opt => opt.MapFrom(o => o.Status.ToString()));
        CreateMap<Card, GetCardDto>();
    }
}
