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
        public DbSet<VillaNumber> VillaNumbers { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // --- Seed Villas ---
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
                },
                new Villa
                {
                    Id = 3,
                    Name = "Luxury Garden Villa",
                    Details = "Spacious villa with private garden",
                    Price = 250.0,
                    Sqft = 600,
                    Occupancy = 5,
                    ImageUrl = "",
                    Created_Date = new DateTime(2024, 02, 10)
                },
                new Villa
                {
                    Id = 4,
                    Name = "Family Villa",
                    Details = "Perfect for family vacation stays",
                    Price = 180.0,
                    Sqft = 500,
                    Occupancy = 6,
                    ImageUrl = "",
                    Created_Date = new DateTime(2024, 03, 01)
                },
                new Villa
                {
                    Id = 5,
                    Name = "Honeymoon Suite Villa",
                    Details = "Romantic villa with ocean view and jacuzzi",
                    Price = 350.0,
                    Sqft = 450,
                    Occupancy = 2,
                    ImageUrl = "",
                    Created_Date = new DateTime(2024, 04, 05)
                }
            );

            // --- Seed VillaNumbers ---
            modelBuilder.Entity<VillaNumber>().HasData(
                // Existing
                new VillaNumber { VillaNo = 101, VillaId = 1, SpecialDetails = "This is the Royal Villa Number 101" },
                new VillaNumber { VillaNo = 102, VillaId = 1, SpecialDetails = "This is the Royal Villa Number 102" },
                new VillaNumber { VillaNo = 201, VillaId = 2, SpecialDetails = "This is the Premium Villa Number 201" },
                new VillaNumber { VillaNo = 202, VillaId = 2, SpecialDetails = "This is the Premium Villa Number 202" },

                // New ones
                new VillaNumber { VillaNo = 301, VillaId = 3, SpecialDetails = "Luxury Garden Villa No. 301" },
                new VillaNumber { VillaNo = 302, VillaId = 3, SpecialDetails = "Luxury Garden Villa No. 302" },
                new VillaNumber { VillaNo = 401, VillaId = 4, SpecialDetails = "Family Villa No. 401" },
                new VillaNumber { VillaNo = 402, VillaId = 4, SpecialDetails = "Family Villa No. 402" },
                new VillaNumber { VillaNo = 501, VillaId = 5, SpecialDetails = "Honeymoon Suite Villa No. 501" },
                new VillaNumber { VillaNo = 502, VillaId = 5, SpecialDetails = "Honeymoon Suite Villa No. 502" }
            );

            modelBuilder.Entity<Amenity>().HasData(
                new Amenity { Id = 1, Name = "Free Wi-Fi", Description = "High-speed wireless internet access", VillaId = 1 },
                new Amenity { Id = 2, Name = "Swimming Pool", Description = "Outdoor pool with sun loungers", VillaId = 1 },
                new Amenity { Id = 3, Name = "Air Conditioning", Description = "Central air conditioning system", VillaId = 2 },
                new Amenity { Id = 4, Name = "Kitchenette", Description = "Compact kitchen area with appliances", VillaId = 2 },
                new Amenity { Id = 5, Name = "Private Garden", Description = "Secluded garden area with seating", VillaId = 3 },
                new Amenity { Id = 6, Name = "BBQ Grill", Description = "Outdoor barbecue grill for cooking", VillaId = 3 },
                new Amenity { Id = 7, Name = "Kids Play Area", Description = "Designated play area for children", VillaId = 4 },
                new Amenity { Id = 8, Name = "Game Console", Description = "Video game console with games", VillaId = 4 },
                new Amenity { Id = 9, Name = "Jacuzzi", Description = "Private jacuzzi with ocean view", VillaId = 5 },
                new Amenity { Id = 10, Name = "Romantic Decor", Description = "Special romantic decorations", VillaId = 5 }
            );
        }

    }
}
