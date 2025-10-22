using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagicVilla.Domain.Entities;

namespace MagicVilla.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Villa> Villas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Villa>().HasData(
                    new Villa
                    {
                        Id = 1,
                        Name = "Royal Villa",
                        Details = "This is the Royal Villa",
                        Price = 200.0,
                        Sqft = 550,
                        Occupancy = 4,
                        ImageUrl = "",
                        Created_Date = new DateTime(2024, 01, 01)
                    },
                    new Villa
                    {
                        Id = 2,
                        Name = "Premium Pool Villa",
                        Details = "This is the Premium Pool Villa",
                        Price = 300.0,
                        Sqft = 550,
                        Occupancy = 4,
                        ImageUrl = "",
                        Created_Date = new DateTime(2024, 01, 01)
                    }
                );
        }
    }
}
