using AutoMapper;
using Contacts.Api.Dtos;
using Contacts.Api.Models;

namespace Contacts.Api.Mappings
{
    public class TagProfile : Profile
    {
        public TagProfile()
        {
            CreateMap<Tag, TagDto>();
            CreateMap<Tag, TagDetailDto>();
        }
    }
}