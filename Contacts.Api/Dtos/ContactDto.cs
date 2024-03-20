using Contacts.Api.Models;

namespace Contacts.Api.Dtos
{
    public class ContactDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public bool IsBookmarked { get; set; }
    }

    public class ContactDetailDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public bool IsBookmarked { get; set; }
        public List<ContactTag> ContactTags { get; set; } = new List<ContactTag>();
        public List<Email> Emails { get; set; } = new List<Email>();
        public List<Number> Numbers { get; set; } = new List<Number>();
    }
}
