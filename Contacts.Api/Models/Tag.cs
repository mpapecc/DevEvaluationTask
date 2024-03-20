using FluentValidation;
using System.ComponentModel.DataAnnotations;
﻿using System.Text.Json.Serialization;

namespace Contacts.Api.Models
{
    public class Tag:BaseEntity
    {
        [MaxLength(200)]
        public string Value { get; set; }
        [JsonIgnore]
        public List<ContactTag> ContactTags { get; set; } = new List<ContactTag>();
    }

    public class TagValidator : AbstractValidator<Tag>
    {
        public TagValidator()
        {
            RuleFor(competition => competition.Value).NotNull().NotEmpty();
        }
    }
}
