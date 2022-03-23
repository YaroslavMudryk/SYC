using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SYC.Core.Models;
using SYC.Core.Storage.EntityFramework.Extensions;

namespace SYC.Core.Storage.EntityFramework.Configuration
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(s => s.Phones).HasConversion(
                v => v.ToJson(),
                v => v.FromJson<string[]>());
            builder.Property(s => s.Emails).HasConversion(
                v => v.ToJson(),
                v => v.FromJson<string[]>());
            builder.Property(s => s.BankCards).HasConversion(
                v => v.ToJson(),
                v => v.FromJson<string[]>());
            builder.Property(s => s.CarNumbers).HasConversion(
                v => v.ToJson(),
                v => v.FromJson<string[]>());
        }
    }
}
