using Microsoft.EntityFrameworkCore;
using ServiceUser.Models;

namespace ServiceUser.Data;


    public class UserDbContext : DbContext
    {
    public UserDbContext(DbContextOptions<UserDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users {  get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                Username = "Georgie",
                Email = "Georgie@domain.com",
                PasswordHash = "hashed_password",
                FirstName = "Georgie",
                LastName = "Don hustler",
                RegisteredDate = DateTime.Now.AddMonths(-6)
            },
            new User
            {
                Id = 2,
                Username = "Kim",
                Email = "Kim@domain.com",
                PasswordHash = "hashed_password",
                FirstName = "Kim",
                LastName = "Don Viber",
                RegisteredDate = DateTime.Now.AddMonths(-3)
            });
    
    } 
    
}

