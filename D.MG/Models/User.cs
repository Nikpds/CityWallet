using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class User : EntityBase
    {
        public User()
        {
            Debts = new HashSet<Debt>();
            Address = new Address();
        }

        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Vat { get; set; }
        public string Password { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }

        public virtual ICollection<Debt> Debts { get; set; }
    }

    public class UserMap
    {
        public UserMap(EntityTypeBuilder<User> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.Vat)
                         .IsRequired()
                         .HasAnnotation("unique", true);
            entityBuilder.Property(t => t.Name)
                         .IsRequired();
            entityBuilder.Property(t => t.Lastname)
                         .IsRequired();
            entityBuilder.Property(t => t.Password)
                         .IsRequired();
            entityBuilder.Property(t => t.Email)
                         .IsRequired()
                         .HasAnnotation("unique", true);
            entityBuilder.HasMany(x => x.Debts)
                         .WithOne(w => w.User)
                         .HasForeignKey(h => h.UserId)
                         .IsRequired();
            entityBuilder.HasOne(x => x.Address)
                         .WithOne(b=>b.User)
                         .HasForeignKey<Address>(b=>b.UserId)
                         .IsRequired()
                         .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
