using AutoMapper;
using Contacts.Api.Dtos;
using Contacts.Api.Models;

namespace Contacts.Api.Mappings
{
    public class ContactProfile : Profile
    {
        public ContactProfile()
        {
            CreateMap<Contact, ContactDto>();
            CreateMap<Contact, ContactDetailDto>();
        }
    }
}