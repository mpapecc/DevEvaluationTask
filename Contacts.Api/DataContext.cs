using Contacts.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Contacts.Api
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) :base(options)
        {
            
        }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ContactTag> ContactTags { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<Number> Numbers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //ContactTags
            modelBuilder.Entity<ContactTag>()
                .HasOne(c => c.Contact)
                .WithMany(ct => ct.ContactTags)
                .HasForeignKey(c => c.ContactId);

            modelBuilder.Entity<ContactTag>()
                .HasOne(t => t.Tag)
                .WithMany(ct => ct.ContactTags)
                .HasForeignKey(t => t.TagId);

            //Contacts
            modelBuilder.Entity<Contact>()
                .HasMany(c=>c.Emails)
                .WithOne(e=>e.Contact)
                .HasForeignKey(c=>c.ContactId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Contact>()
                .HasMany(c => c.Numbers)
                .WithOne(e => e.Contact)
                .HasForeignKey(c => c.ContactId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Contact>()
                .Navigation(e => e.ContactTags)
                .AutoInclude();

            //Tags
            modelBuilder.Entity<Tag>()
                .HasMany(t => t.ContactTags)
                .WithOne(ct => ct.Tag)
                .HasForeignKey(t=>t.TagId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
