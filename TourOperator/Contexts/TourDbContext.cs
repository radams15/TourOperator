using Microsoft.EntityFrameworkCore;
using TourOperator.Models;
using TourOperator.Models.Entities;

namespace TourOperator.Contexts;

public class TourDbContext : DbContext
{ 
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<Operator> Operators { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Tour> Tours { get; set; }
    public DbSet<BasketItem> BasketItems { get; set; }
    
    public TourDbContext(DbContextOptions<TourDbContext> options)
        : base(options)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Hotel>()
            .HasMany(h => h.Rooms)
            .WithOne(r => r.Hotel)
            .HasForeignKey(r => r.HotelId)
            .HasPrincipalKey(h => h.Id);
        
        DbModelCreator.CreateModels(modelBuilder);
    }
}