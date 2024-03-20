using Contacts.Api.Dtos;
using FluentValidation;
using System.ComponentModel.DataAnnotations;

﻿namespace Contacts.Api.Models
{
    public class Contact:BaseEntity
    {
        [Required]
        [MaxLength(200)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(200)]
        public string LastName { get; set; }

        [MaxLength(200)]
        public string Address { get; set; }
        public bool IsBookmarked { get; set; }
        public List<ContactTag> ContactTags { get; set; } = new List<ContactTag>();
        public List<Email> Emails { get; set; } = new List<Email>();
        public List<Number> Numbers { get; set; } = new List<Number>();
    }

    public class ContactValidator : AbstractValidator<Contact>
    {
        public ContactValidator()
        {
            RuleFor(competition => competition.FirstName).NotNull().NotEmpty();
            RuleFor(competition => competition.LastName).NotNull().NotEmpty();
            RuleForEach(competition => competition.Emails).ChildRules(email =>
            {
                email.RuleFor(x => x.Value).EmailAddress();
            });
            RuleForEach(competition => competition.Numbers).ChildRules(number =>
            {
                number.RuleFor(x => x.Value).Matches("^(?:([0-9]{1})*[- .(]*([0-9]{3})[- .)]*[0-9]{3}[- .]*[0-9]{4})+$");
            });
        }
    }
}
