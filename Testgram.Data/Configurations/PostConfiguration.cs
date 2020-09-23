using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Testgram.Core.Models;

namespace Testgram.Data.Configurations
{
    class PostConfigurations : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> entity)
        {
            entity.Property(e => e.PostId)
                    .HasColumnName("post_id")
                    .ValueGeneratedNever();

            entity.Property(e => e.Content)
                .IsRequired()
                .HasColumnName("content")
                .HasMaxLength(2048);

            entity.Property(e => e.PostDate)
                .HasColumnName("post_date")
                .HasColumnType("datetime");

            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User)
                .WithMany(p => p.Post)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Post_fk0");
        }
    }
}
