using System;
using System.Data.Common;
using Common.Entities;
using Microsoft.EntityFrameworkCore;
namespace Common;

public class CarRentalDbContext: DbContext
{
    public CarRentalDbContext(DbContextOptions<CarRentalDbContext> options) 
        : base(options)
    {
        
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Roles> Roles { get; set; }
    public DbSet<Car> Cars { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Feature> Features { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Rental> Rentals { get; set; }
    public DbSet<Payment> Payments { get; set; }

    public DbSet<RentalStatusHistory> RentalStatusHistories{get;set;}

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
       
        modelBuilder.Entity<CarCategory>().HasKey(cc => new { cc.CarId, cc.CategoryId });
        modelBuilder.Entity<CarFeature>().HasKey(cf => new { cf.CarId, cf.FeatureId });
        modelBuilder.Entity<CarLocation>().HasKey(cl => new { cl.CarId, cl.LocationId });
        modelBuilder.Entity<UserRoles>().HasKey(ur => new { ur.UserId, ur.RoleId });

        
        modelBuilder.Entity<CarCategory>()
            .HasOne(cc => cc.Car)
            .WithMany(c => c.CarCategories)
            .HasForeignKey(cc => cc.CarId);

        modelBuilder.Entity<CarCategory>()
            .HasOne(cc => cc.Category)
            .WithMany(c => c.CarCategories)
            .HasForeignKey(cc => cc.CategoryId);

        
        modelBuilder.Entity<CarFeature>()
            .HasOne(cf => cf.Car)
            .WithMany(c => c.CarFeatures)
            .HasForeignKey(cf => cf.CarId);

        modelBuilder.Entity<CarFeature>()
            .HasOne(cf => cf.Feature)
            .WithMany(f => f.CarFeatures)
            .HasForeignKey(cf => cf.FeatureId);

        
        modelBuilder.Entity<CarLocation>()
            .HasOne(cl => cl.Car)
            .WithMany(c => c.CarLocations)
            .HasForeignKey(cl => cl.CarId);

        modelBuilder.Entity<CarLocation>()
            .HasOne(cl => cl.Location)
            .WithMany(l => l.CarLocations)
            .HasForeignKey(cl => cl.LocationId);

        
        modelBuilder.Entity<UserRoles>()
            .HasOne(ur => ur.User)
            .WithMany(u => u.Roles)
            .HasForeignKey(ur => ur.UserId);

        modelBuilder.Entity<UserRoles>()
            .HasOne(ur => ur.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(ur => ur.RoleId);

        
        modelBuilder.Entity<Rental>()
            .HasOne(r => r.User)
            .WithMany(u => u.Rentals)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Rental>()
            .HasOne(r => r.Car)
            .WithMany(c => c.Rentals)
            .HasForeignKey(r => r.CarId)
            .OnDelete(DeleteBehavior.Restrict);

        
        modelBuilder.Entity<Rental>()
            .HasOne(r => r.PickupLocation)
            .WithMany(l => l.PickupRentals)
            .HasForeignKey(r => r.PickupLocationId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Rental>()
            .HasOne(r => r.ReturnLocation)
            .WithMany(l => l.ReturnRentals)
            .HasForeignKey(r => r.ReturnLocationId)
            .OnDelete(DeleteBehavior.Restrict);
       
        modelBuilder.Entity<Payment>()
            .HasOne(p => p.Rental)
            .WithMany(r => r.Payments)
            .HasForeignKey(p => p.RentalId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Car>()
            .Property(c => c.PricePerDay)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Payment>()
            .Property(p => p.Amount)
            .HasPrecision(18, 2);


        modelBuilder.Entity<RentalStatusHistory>()
        .HasOne(rsh=>rsh.Rental)
        .WithMany(rsh=>rsh.StatusHistory)
        .HasForeignKey(rsh=>rsh.RentalId)
        .OnDelete(DeleteBehavior.Restrict);
            
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                FirstName = "Admin",
                LastName = "Adminov",
                Username = "admin",
                Password = "admin123"
            }
        );

        modelBuilder.Entity<Roles>().HasData(
            new Roles { Id = 1, RoleName = "Admin" },
            new Roles { Id = 2, RoleName = "Employer" },
            new Roles { Id = 3, RoleName = "Customer" }
        );

        modelBuilder.Entity<UserRoles>().HasData(
            new UserRoles { UserId = 1, RoleId = 1 }
        );

        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Sedan" },
            new Category { Id = 2, Name = "SUV" },
            new Category { Id = 3, Name = "Coupe" },
            new Category { Id = 4, Name = "Hatchback" },
            new Category { Id = 5, Name = "Convertible" },
            new Category { Id = 6, Name = "Minivan" },
            new Category { Id = 7, Name = "Pickup Truck" },
            new Category { Id = 8, Name = "Wagon" },
            new Category { Id = 9, Name = "Limousine"},
            
            new Category{Id=10,Name="Gasoline"},
            new Category{Id=20,Name="Diesel"},
            new Category{Id=30,Name="Electric"},
            new Category{Id=40,Name="Hybrid"},

            new Category{Id=50,Name="Manual"},
            new Category{Id=60,Name="Automatic"}

        );

        modelBuilder.Entity<Feature>().HasData(
            new Feature { Id = 1, Name = "Air Conditioning" },
            new Feature { Id = 2, Name = "GPS Navigation" },
            new Feature { Id = 3, Name = "Bluetooth Connectivity" },
            new Feature { Id = 4, Name = "Heated Seats" },
            new Feature { Id = 5, Name = "Sunroof" },
            new Feature { Id = 6, Name = "Backup Camera" },
            new Feature { Id = 7, Name = "Cruise Control" },
            new Feature { Id = 8, Name = "Keyless Entry" },
            new Feature { Id = 9, Name = "Alloy Wheels" },
            new Feature { Id = 10, Name = "Premium Sound System" },
            new Feature { Id = 11, Name = "Leather Seats" },
            new Feature { Id = 12, Name = "Parking Sensors" },
            new Feature { Id = 13, Name = "Remote Start" },
            new Feature { Id = 14, Name = "Wi-Fi Hotspot" },
            new Feature { Id = 15, Name = "Voice Control" },
            new Feature { Id = 16, Name = "Automatic Emergency Braking" },
            new Feature { Id = 17, Name = "Lane Departure Warning" },
            new Feature { Id = 18, Name = "Blind Spot Monitoring" },
            new Feature { Id = 19, Name = "Adaptive Cruise Control" },
            new Feature { Id = 20, Name = "Tire Pressure Monitoring System" },
            new Feature { Id = 21, Name = "Fog Lights" },
            new Feature { Id = 22, Name = "Roof Rails" },
            new Feature { Id = 23, Name = "Third-Row Seating" },
            new Feature { Id = 24, Name = "All-Wheel Drive" },
            new Feature { Id=25,Name="Lane Assist"},
            new Feature {Id=26,Name="Traffic Sign Recognition"},
            new Feature {Id=27,Name="Head-Up Display"},
            new Feature {Id=28,Name="Night Vision Assist"},
            new Feature {Id=29,Name="360-Degree Camera System"},
            new Feature {Id=30,Name="Electric Vehicle Charging Port"}
        
        );

    }
}
