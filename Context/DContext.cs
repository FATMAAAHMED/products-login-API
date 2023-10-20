using ProductDomain.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ProductDomain;
using ProductDomain.Configuration;


namespace Context
{
    public class DContext : IdentityDbContext<User, IdentityRole<long>, long>
    {
        public DbSet<Product> Product { get; set; }
        //public DbSet<User> User { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new ProductConfiguration().Configure(modelBuilder.Entity<Product>());

            base.OnModelCreating(modelBuilder);
        }
        public DContext(DbContextOptions<DContext> options) : base(options)
        {

        }
    }
}