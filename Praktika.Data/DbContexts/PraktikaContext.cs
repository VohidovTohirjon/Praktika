using Microsoft.EntityFrameworkCore;
using Praktika.Domain.Entities;

namespace Praktika.Data.DbContexts
{
    public class PraktikaContext : DbContext
    {
        public virtual DbSet<Content> Contents { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public PraktikaContext(DbContextOptions<PraktikaContext> options)
            : base(options)
        {












        }

        /*     protected override void OnModelCreating(ModelBuilder modelBuilder)
             {
                 modelBuilder.Entity<Course>()
                     .HasKey(bc => new { bc.UserId, bc.ContentId });
                 modelBuilder.Entity<Course>()
                     .HasOne(bc => bc.User)
                     .WithMany(b => b.Courses)
                     .HasForeignKey(bc => bc.UserId);
                 modelBuilder.Entity<Course>()
                     .HasOne(bc => bc.Content)
                     .WithMany(c => c.Courses)
                     .HasForeignKey(bc => bc.ContentId);
             }*/
    }
}
