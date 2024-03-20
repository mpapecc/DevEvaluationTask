using Contacts.Api.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Contacts.Api.Dtos
{
    public class TagDto
    {
        public Guid Id { get; set; }
        public string Value { get; set; }
    }

    public class TagDetailDto
    {
        public Guid Id { get; set; }
        public string Value { get; set; }
        public List<ContactTag> ContactTags { get; set; } = new List<ContactTag>();
    }
}
