using System.Text.Json.Serialization;

namespace Contacts.Api.Models
{
    public class ContactTag:BaseEntity
    {
        public Guid ContactId { get; set; }   
        public Guid TagId { get; set; }
        [JsonIgnore]
        public Contact? Contact { get; set; }
        public Tag? Tag { get; set; }
    }
}
