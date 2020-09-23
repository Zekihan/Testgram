using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Testgram.Core.Models;

namespace Testgram.Data.Configurations
{
    class ProfileConfigurations : IEntityTypeConfiguration<Profile>
    {
        public void Configure(EntityTypeBuilder<Profile> entity)
        {
            entity.HasKey(e => e.UserId)
                    .HasName("PK_PROFILE");

            entity.HasIndex(e => e.Email)
                .HasName("UQ__Profile__AB6E6164DBF924B2")
                .IsUnique();

            entity.HasIndex(e => e.Username)
                .HasName("UQ__Profile__F3DBC57221FDDDF4")
                .IsUnique();

            entity.Property(e => e.UserId)
                .HasColumnName("user_id")
                .ValueGeneratedNever();

            entity.Property(e => e.Biografy)
                .HasColumnName("biografy")
                .HasMaxLength(255);

            entity.Property(e => e.Email)
                .IsRequired()
                .HasColumnName("email")
                .HasMaxLength(255);

            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasColumnName("first_name")
                .HasMaxLength(255);

            entity.Property(e => e.LastName)
                .IsRequired()
                .HasColumnName("last_name")
                .HasMaxLength(255);

            entity.Property(e => e.Username)
                .IsRequired()
                .HasColumnName("username")
                .HasMaxLength(50);
        }
    }
}
