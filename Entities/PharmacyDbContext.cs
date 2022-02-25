using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MyPharmacy.Entities
{
    public class PharmacyDbContext : DbContext
    {
        
        public DbSet<Pharmacy> Pharmacies { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Drug> Drugs { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        
        public DbSet<Status> Statuses { get; set; }
        public DbSet<DrugInformation> DrugInformations { get; set; }
        public DbSet<OrderByClient> OrderByClients { get; set; }
        public DbSet<DrugCategory> DrugCategories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Status>()
                .Property(s => s.Name)
                .IsRequired();

            modelBuilder.Entity<OrderByClient>()
                .Property(o => o.IsPersonalPickup)
                .IsRequired();

            modelBuilder.Entity<Role>()
                .Property(u => u.Name)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired();

            modelBuilder.Entity<DrugCategory>()
                .Property(u => u.CategoryName)
                .IsRequired();

            modelBuilder.Entity<Pharmacy>()
                .Property(p => p.Name)
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
            if(!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.Development.json")
                    .Build();
                var connectionString = configuration.GetConnectionString("PharmacyDbConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}
