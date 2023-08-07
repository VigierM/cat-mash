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
            CreateMap<Cat, CatDTO>().ReverseMap();
            CreateMap<PagedList<Cat>, PagedList<CatDTO>>();
        }
    }
}
