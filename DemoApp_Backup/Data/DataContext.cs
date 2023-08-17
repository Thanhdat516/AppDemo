﻿using DemoApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Company> Companies => Set<Company>();
        public DbSet<Employee> Employees => Set<Employee>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Write Fluent API configurations here

            //Property Configurations
            modelBuilder.Entity<Employee>()
                .HasOne(b => b.Company)
                .WithMany(a => a.Employees)
                .HasForeignKey(e => e.companyID)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
