using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.DB
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
                optionsBuilder.UseSqlServer("DESKTOP-K9L2Q5V\\SQLEXPRESS;Database=WebApiWeatherInfo;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>()
                        .HasOne(e => e.Country)
                        .WithMany(e => e.Cities)
                        .HasForeignKey(e => e.CountryId)
                        .OnDelete(DeleteBehavior.Cascade); ;
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
    }
}
