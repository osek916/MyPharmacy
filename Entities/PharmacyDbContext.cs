using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPharmacy.Entities
{
    public class PharmacyDbContext : DbContext
    {
        private string _connectionString = "Server=(localdb)\\mssqllocaldb;Database=PharmacyDb;Trusted_Connection=True;";
        public DbSet<Pharmacy> Pharmacies { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Drug> Drugs { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pharmacy>()
                .Property(p => p.Name)
                .IsRequired();

            modelBuilder.Entity<Drug>()
                .Property(d => d.DrugsName)
                .IsRequired();

            modelBuilder.Entity<Address>()
                .Property(a1 => a1.City)
                .IsRequired()
                .HasMaxLength(30);

            modelBuilder.Entity<Address>()
                .Property(a2 => a2.Street)
                .IsRequired();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
