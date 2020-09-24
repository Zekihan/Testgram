using Microsoft.EntityFrameworkCore;
using Testgram.Core.Models;

namespace Testgram.Data.Configurations
{
    public partial class SocialContext : DbContext
    {
        public SocialContext()
        {
        }

        public SocialContext(DbContextOptions<SocialContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<Follow> Follow { get; set; }
        public virtual DbSet<Likes> Likes { get; set; }
        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<Profile> Profile { get; set; }

        //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //        {
        //            if (!optionsBuilder.IsConfigured)
        //            {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
        //                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Social;Trusted_Connection=True;");
        //            }
        //        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CommentConfiguration());

            modelBuilder.ApplyConfiguration(new FollowConfiguration());

            modelBuilder.ApplyConfiguration(new LikeConfiguration());

            modelBuilder.ApplyConfiguration(new PostConfigurations());

            modelBuilder.ApplyConfiguration(new ProfileConfigurations());

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}