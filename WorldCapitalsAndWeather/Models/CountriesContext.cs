using System.IO;
using Microsoft.EntityFrameworkCore;

namespace WorldCapitalsAndWeather.Models
{
    public partial class CountriesContext : DbContext
    {
        public virtual DbSet<Country> Country { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var currentDir = Directory.GetCurrentDirectory();
            var dbPath = Path.Combine(currentDir, "App_Data\\Countries.mdf");

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='" + dbPath + "';Integrated Security=True;Connect Timeout=30");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.Capital)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RegionId)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });
        }
    }
}
