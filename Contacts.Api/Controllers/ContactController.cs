using AutoMapper;
using AutoMapper.QueryableExtensions;
using Contacts.Api.Dtos;
using Contacts.Api.Models;
using Contacts.Api.Repositories.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace Contacts.Api.Controllers
{

    public class ContactController :BaseController<Contact>
    {
        private readonly IBaseRepository<Email> _emailRepository;
        private readonly IBaseRepository<Number> _numberRepository;
        private readonly IBaseRepository<Contact> _contactRepository;
        private readonly IBaseRepository<ContactTag> _contactTagRepository;
        private readonly IMapper _mapper;

        public ContactController(
            IBaseRepository<Contact> contactRepository, 
            IBaseRepository<Email> emailRepository,
            IBaseRepository<Number> numberRepository,
            IBaseRepository<ContactTag> contactTagRepository,
            IMapper mapper
            ) : base(contactRepository)
        {
            _contactRepository = contactRepository;
            _emailRepository = emailRepository;
            _numberRepository = numberRepository;
            _contactTagRepository = contactTagRepository;
            _mapper = mapper;
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search( [FromQuery] List<Guid> tags, [FromQuery] string? firstName = null, [FromQuery] string? lastName = null)
        {

            var query = _contactRepository.GetAll();
            if (firstName != null)
            {
                query = query.Where(ContactFirstNameQuery(firstName));
            }

            if (lastName != null)
            {
                query = query.Where(ContactLastNameQuery(lastName));
            }

            if (tags.Any())
            {
                tags.ForEach(tag => query = query.Where(ContactContainsTagQuery(tag)));
                
            }

            var result = (await query.OrderBy(c => c.FirstName)
                .AsNoTracking()
                .ProjectTo<ContactDto>(_mapper.ConfigurationProvider)
                .ToListAsync());

            return Ok(result);
        }

        [HttpGet("{id}")]
        public override async Task<IActionResult> Find(Guid id)
        {
            var record = await _contactRepository.GetById(id)
                .Include(c => c.Emails)
                .Include(c => c.Numbers)
                .Include(c => c.ContactTags)
                .ThenInclude(ct=>ct.Tag)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (record == null)
                return NotFound();

            return Ok(_mapper.Map<ContactDetailDto>(record));
        }

        [HttpPut("{id}")]
        public override async Task<IActionResult> Update(Guid id, [FromBody] Contact record)
        {
            if (id != record.Id)
                return BadRequest();

            await UpdateContactEmails(id, record);
            await UpdateContactNumbers(id, record);
            await UpdateContactTags(id, record);
            record.Emails.Clear();
            record.Numbers.Clear();
            record.ContactTags.Clear();

            await _contactRepository.UpdateAsync(record);
            return Ok();
        }

        private async Task UpdateContactEmails(Guid id, Contact record)
        {
            List<Email> allContactEmails = _emailRepository.GetAll().Where(e => e.ContactId == id).ToList();

            List<Email> emailsForDelete = new List<Email>();
            List<Email> emailsForUpdate = new List<Email>();
            List<Email> emailsForAdd = new List<Email>();

            int i = -1;
            foreach (Email email in allContactEmails)
            {
                i++;
                var emailRecord = record.Emails.Where(e => e.Id == email.Id);
                if (!emailRecord.Any())
                {
                    emailsForDelete.Add(email);
                    continue;
                }
                email.Value = emailRecord.First().Value;
                emailsForUpdate.Add(email);
            }

            foreach (Email email in record.Emails)
            {
                if (!allContactEmails.Where(e => e.Id == email.Id).Any())
                {
                    emailsForAdd.Add(email);
                }
            }

            if (emailsForDelete.Any())
            {
                await _emailRepository.DeleteRangeAsync(emailsForDelete);
            }
            if (emailsForUpdate.Any())
            {
                await _emailRepository.UpdateRangeAsync(emailsForUpdate);
            }
            if (emailsForAdd.Any())
            {
                await _emailRepository.UpdateRangeAsync(emailsForAdd);
            }
        }

        private async Task UpdateContactNumbers(Guid id, Contact record)
        {
            List<Number> allContactNumbers = _numberRepository.GetAll().Where(e => e.ContactId == id).ToList();

            List<Number> numbersForDelete = new List<Number>();
            List<Number> emailsForUpdate = new List<Number>();
            List<Number> emailsForAdd = new List<Number>();

            int i = -1;
            foreach (Number number in allContactNumbers)
            {
                i++;
                var numberRecord = record.Numbers.Where(e => e.Id == number.Id);
                if (!numberRecord.Any())
                {
                    numbersForDelete.Add(number);
                    continue;
                }
                number.Value = numberRecord.First().Value;
                emailsForUpdate.Add(number);
            }

            foreach (Number number in record.Numbers)
            {
                if (!allContactNumbers.Where(e => e.Id == number.Id).Any())
                {
                    emailsForAdd.Add(number);
                }
            }

            if (numbersForDelete.Any())
            {
                await _numberRepository.DeleteRangeAsync(numbersForDelete);
            }
            if (emailsForUpdate.Any())
            {
                await _numberRepository.UpdateRangeAsync(emailsForUpdate);
            }
            if (emailsForAdd.Any())
            {
                await _numberRepository.UpdateRangeAsync(emailsForAdd);
            }
        }
         
        private async Task UpdateContactTags(Guid id, Contact record)
        {
            List<ContactTag> allContactTags = _contactTagRepository.GetAll().Where(ct => ct.ContactId == id).ToList();
            List<ContactTag> contactTagsForDelete = new List<ContactTag>();
            List<ContactTag> contactTagsForAdd = new List<ContactTag>();

            foreach (var contactTag in allContactTags)
            {
                var contactTagRecord = record.ContactTags.Where(ct => ct.TagId == contactTag.TagId);
                if (!contactTagRecord.Any())
                {
                    contactTagsForDelete.Add(contactTag);
                }
            }

            foreach (ContactTag contactTag in record.ContactTags)
            {
                if (!allContactTags.Where(ct => ct.TagId == contactTag.TagId).Any())
                {
                    contactTag.ContactId = id;
                    contactTagsForAdd.Add(contactTag);
                }
            }

            if (contactTagsForDelete.Any())
            {
                await _contactTagRepository.DeleteRangeAsync(contactTagsForDelete);
            }

            if (contactTagsForAdd.Any())
            {

                await _contactTagRepository.AddRangeAsync(contactTagsForAdd);
            }
        }

        private Expression<Func<Contact, bool>> ContactContainsTagQuery(Guid tag)
        {

            return c => c.ContactTags.Any(ct => ct.TagId == tag);
        }

        private Expression<Func<Contact, bool>> ContactFirstNameQuery(string firstName)
        {

            return contact => contact.FirstName.Contains(firstName);
        }

        private Expression<Func<Contact, bool>> ContactLastNameQuery(string lastName)
        {

            return contact => contact.LastName.Contains(lastName);
        }

    }
}
