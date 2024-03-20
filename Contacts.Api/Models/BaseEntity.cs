using System.ComponentModel.DataAnnotations;

namespace Contacts.Api.Models
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
        public DateTime LastUpdateDate { get; set; } = DateTime.UtcNow;
    }
}
