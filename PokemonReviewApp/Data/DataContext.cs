using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)//base is push data in dbcontext class
        {

        }

        //===========Tells the Dbcontext What are the tables===============
        public DbSet<Category> Categories{ get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Pokemon> Pokemons { get; set; }
        public DbSet<PokemanOwner> PokemanOwners { get; set; }
        public DbSet<PokemanCategory> PokemanCategories { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Reviewer> Reviewers { get; set; }

        //===============for many to many table still have to  use onModelCreating.
        //sometime we need to customize the table certain time so OnModelCreating customize the tables.
        //Manipulating database code have alot of advantages
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //this will tell entity framework we need to link these two ids otherwis relationship is not going exists
            //this is many to many relationship between pokeman and category
            modelBuilder.Entity<PokemanCategory>()
                    .HasKey(pc => new { pc.PokemanId, pc.CategoryId });
            modelBuilder.Entity<PokemanCategory>()
                   .HasOne(p => p.Pokeman)
                   .WithMany(pc => pc.PokemanCategories)
                   .HasForeignKey(p=>p.PokemanId);
            modelBuilder.Entity<PokemanCategory>()
                    .HasOne(p => p.Pokeman)
                    .WithMany(pc => pc.PokemanCategories)
                    .HasForeignKey(c => c.CategoryId);

            //====================many to many relationship between pokeman and owner
            modelBuilder.Entity<PokemanOwner>()
                   .HasKey(po => new { po.PokemanId, po.OwnerId });
            modelBuilder.Entity<PokemanOwner>()
                   .HasOne(p => p.Pokemon)
                   .WithMany(po => po.PokemanOwners)
                   .HasForeignKey(p=>p.PokemanId);

            modelBuilder.Entity<PokemanOwner>()
                    .HasOne(p => p.Pokemon)
                    .WithMany(po => po.PokemanOwners)
                    .HasForeignKey(o=>o.OwnerId);

        }


    }
}
