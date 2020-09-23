using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Testgram.Core.Models;

namespace Testgram.Data.Configurations
{
    class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> entity)
        {
            entity.Property(e => e.CommentId)
                                .HasColumnName("comment_id")
                                .ValueGeneratedNever();

            entity.Property(e => e.CommentDate)
                .HasColumnName("comment_date")
                .HasColumnType("datetime");

            entity.Property(e => e.Content)
                .IsRequired()
                .HasColumnName("content")
                .HasMaxLength(2048);

            entity.Property(e => e.ParentComment).HasColumnName("parent_comment");

            entity.Property(e => e.PostId).HasColumnName("post_id");

            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.ParentCommentNavigation)
                .WithMany(p => p.InverseParentCommentNavigation)
                .HasForeignKey(d => d.ParentComment)
                .HasConstraintName("Comment_fk0");

            entity.HasOne(d => d.Post)
                .WithMany(p => p.Comment)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Comment_fk2");

            entity.HasOne(d => d.User)
                .WithMany(p => p.Comment)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Comment_fk1");
        }
    }
}
