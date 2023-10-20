
using Microsoft.EntityFrameworkCore;
using ProductDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ProductContext
{
    public static class RelationsMapping
    {
        public static void MapRelation(this ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Product>()
            //    .HasOne(p => p.Image)
            //    .WithOne(i => i.Product)
            //    .HasForeignKey<Product>(p=>p.ImageId);

            //modelBuilder.Entity<Product>()
            //    .HasOne(p => p.ProductCategory)
            //    .WithMany(c => c.Products)
            //    //.HasForeignKey(p => p.)
            //    .OnDelete(DeleteBehavior.Cascade);




        }
    }
}
