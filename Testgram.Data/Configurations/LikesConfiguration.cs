using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Testgram.Core.Models;

namespace Testgram.Data.Configurations
{
    class LikeConfiguration : IEntityTypeConfiguration<Likes>
    {
        public void Configure(EntityTypeBuilder<Likes> entity)
        {
            entity.HasKey(e => new { e.UserId, e.PostId })
                    .HasName("PK_LIKES");

            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.Property(e => e.PostId).HasColumnName("post_id");

            entity.Property(e => e.LikeDate)
                .HasColumnName("like_date")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Post)
                .WithMany(p => p.Likes)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Likes_fk1");

            entity.HasOne(d => d.User)
                .WithMany(p => p.Likes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Likes_fk0");
        }
    }
}
