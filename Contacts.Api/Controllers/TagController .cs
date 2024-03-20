using AutoMapper;
using AutoMapper.QueryableExtensions;
using Contacts.Api.Dtos;
using Contacts.Api.Models;
using Contacts.Api.Repositories.Abstraction;
using Contacts.Api.Repositories.Implementation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Contacts.Api.Controllers
{

    public class TagController : BaseController<Tag>
    {
        private readonly IBaseRepository<Tag> _tagRepository;
        private readonly IMapper _mapper;
        public TagController(IBaseRepository<Tag> tagRepository,IMapper mapper) : base(tagRepository)
        {
            _tagRepository = tagRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public override async Task<IActionResult> GetAll()
        {
            var result = await _tagRepository.GetAll()
                .AsNoTracking()
                .ProjectTo<TagDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return Ok(result);
        }
    }
}
