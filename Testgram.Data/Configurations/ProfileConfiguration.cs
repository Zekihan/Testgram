using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Testgram.Core.Models;

namespace Testgram.Data.Configurations
{
    internal class ProfileConfigurations : IEntityTypeConfiguration<Profile>
    {
        public void Configure(EntityTypeBuilder<Profile> entity)
        {
            entity.HasKey(e => e.UserId)
                    .HasName("PK_PROFILE");

            entity.HasIndex(e => e.Email)
                .HasName("UQ__Profile__AB6E61642272DBD6")
                .IsUnique();

            entity.HasIndex(e => e.Username)
                .HasName("UQ__Profile__F3DBC57207E43CE6")
                .IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");

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