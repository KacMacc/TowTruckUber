using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TowTruckUberAPI.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<MapGrid> MapGrids { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Trip>(b => { 
                b.HasOne(x => x.Customer)
                .WithMany(y => y.CustomerTrips);

                b.HasOne(x => x.Contractor)
                    .WithMany(y => y.ContractorTrips);
            });
        }

    }
}
