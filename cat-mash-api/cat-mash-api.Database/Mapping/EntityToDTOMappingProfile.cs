using AutoMapper;
using cat_mash_api.Database.Shared.EntityModels;
using cat_mash_api.Domain.Models.DTOs;
using cat_mash_api.Domain.Models;

namespace cat_mash_api.Database.Mapping
{
    public class EntityToDTOMappingProfile : Profile
    {
        public EntityToDTOMappingProfile()
        {
            CreateMap<Cat, CatDTO>()
                .ForMember(d => d.Votes, opt => opt.MapFrom(src => src.Votes.Count()))
                .ReverseMap();
            CreateMap<PagedList<Cat>, PagedList<CatDTO>>();
        }
    }
}
