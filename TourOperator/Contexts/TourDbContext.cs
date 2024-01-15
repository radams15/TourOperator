namespace TourOperator.Contexts;

using Microsoft.EntityFrameworkCore;
using Models.Entities;

public class TourDbContext : DbContext
{ 
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<Operator> Operators { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Tour> Tours { get; set; }
    
    public DbSet<Role> Roles { get; set; }
    
    public TourDbContext(DbContextOptions<TourDbContext> options)
        : base(options)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        DbModelCreator.CreateModels(modelBuilder);
        
        modelBuilder.Entity<Customer>()
            .HasIndex(c => c.Username)
            .IsUnique();
    }
}