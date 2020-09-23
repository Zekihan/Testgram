using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Testgram.Core.Models;

namespace Testgram.Data.Configurations
{
    class FollowConfiguration : IEntityTypeConfiguration<Follow>
    {
        public void Configure(EntityTypeBuilder<Follow> entity)
        {
            entity.HasKey(e => new { e.UserId, e.FollowerId })
                    .HasName("PK_FOLLOW");

            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.Property(e => e.FollowerId).HasColumnName("follower_id");

            entity.Property(e => e.FollowDate)
                .HasColumnName("follow_date")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Follower)
                .WithMany(p => p.FollowFollower)
                .HasForeignKey(d => d.FollowerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Follow_fk1");

            entity.HasOne(d => d.User)
                .WithMany(p => p.FollowUser)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Follow_fk0");
        }
    }
}
