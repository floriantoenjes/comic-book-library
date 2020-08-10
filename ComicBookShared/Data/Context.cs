using ComicBookShared.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ComicBookShared.Data
{
    /// <summary>
    /// Entity Framework context class.
    /// </summary>
    public class Context : IdentityDbContext<User>
    {
        public DbSet<ComicBook> ComicBooks { get; set; }
        public DbSet<Series> Series { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public new DbSet<Role> Roles { get; set; }
        public DbSet<ComicBookArtist> ComicBookArtists { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Removing the pluralizing table name convention 
            // so our table names will use our entity class singular names.
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // Using the fluent API to configure the precision and scale
            // for the ComicBook.AverageRating property.
            modelBuilder.Entity<ComicBook>()
                .Property(cb => cb.AverageRating)
                .HasPrecision(5, 2);
        }
    }
}
