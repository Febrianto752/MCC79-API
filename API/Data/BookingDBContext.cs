using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class BookingDBContext : DbContext
{
    public BookingDBContext(DbContextOptions<BookingDBContext> options) : base(options)
    {

    }

    // Table
    public DbSet<Account> Accounts { get; set; }
    public DbSet<AccountRole> AccountRoles { get; set; }
    public DbSet<Education> Educations { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<University> Universities { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Room> Rooms { get; set; }

    // Other Configuration or Fluent API
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Constraints Unique
        modelBuilder.Entity<Employee>()
                    .HasIndex(e => new
                    {
                        e.NIK,
                        e.Email,
                        e.PhoneNumber
                    }).IsUnique();

        // University - Education (One to Many)
        modelBuilder.Entity<University>()
                    .HasMany(university => university.Educations)
                    .WithOne(education => education.University)
                    .HasForeignKey(education => education.UniversityGUID);


        // Employee - Education (One to One)
        modelBuilder.Entity<Employee>()
                    .HasOne(employee => employee.Education)
                    .WithOne(education => education.Employee)
                    .HasForeignKey<Education>(education => education.GUID);

        // Employee - Account (One to One)
        modelBuilder.Entity<Employee>()
                    .HasOne(employee => employee.Account)
                    .WithOne(account => account.Employee)
                    .HasForeignKey<Account>(account => account.GUID);

        // Account - Account Roles (one to many)
        modelBuilder.Entity<Account>()
                    .HasMany(account => account.AccountRoles)
                    .WithOne(accountRole => accountRole.Account)
                    .HasForeignKey(accountRole => accountRole.AccountGUID);

        // Role - Account Roles (one to many)
        modelBuilder.Entity<Role>()
                    .HasMany(role => role.AccountRoles)
                    .WithOne(accountRole => accountRole.Role)
                    .HasForeignKey(accountRole => accountRole.RoleGUID);

        // Employee - Booking (one to many)
        modelBuilder.Entity<Employee>()
                    .HasMany(employee => employee.Bookings)
                    .WithOne(booking => booking.Employee)
                    .HasForeignKey(booking => booking.EmployeeGUID);

        // Room - Booking (one to many)
        modelBuilder.Entity<Room>()
                    .HasMany(room => room.Bookings)
                    .WithOne(booking => booking.Room)
                    .HasForeignKey(booking => booking.RoomGUID);




    }
}