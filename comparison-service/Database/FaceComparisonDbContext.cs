using Microsoft.EntityFrameworkCore;
using FaceComparisonAPI.Models;

namespace FaceComparisonAPI.Database
{
    public class FaceComparisonDbContext : DbContext
    {
        public FaceComparisonDbContext(DbContextOptions<FaceComparisonDbContext> options)
            : base(options)
        {
        }

        public DbSet<AnonymizedFace> AnonymizedFaces { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AnonymizedFace>()
                .HasKey(a => a.Identifier);
            
            modelBuilder.HasDefaultSchema("caf");
        }
    }
}
