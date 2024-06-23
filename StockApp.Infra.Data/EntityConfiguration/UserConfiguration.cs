using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockApp.Domain.Entities;

namespace StockApp.Infra.Data.EntityConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Username);
            builder.Property(u => u.Username).HasMaxLength(50).IsRequired();
            builder.Property(u => u.PasswordHash).HasMaxLength(64).IsRequired();
            builder.Property(u => u.Role).HasMaxLength(30).IsRequired();
        }
    }
}