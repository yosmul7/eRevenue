using eRevenue.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eRevenue.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<OrganizationLevel> OrganizationLevels { get; set; }
        public DbSet<Center> Centers { get; set; }
        public DbSet<Year> Years { get; set; }
        public DbSet<RevenuePlan> RevenuePlans { get; set; }

        public DbSet<RevenueTypeDetail> RevenueTypeDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<RevenuePlan>(entity =>
             {
                 entity.HasOne(d => d.Center)
                 .WithMany(p => p.RevenuePlan)
                  .HasForeignKey(d => d.CenterId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK_RevenuePlan_CenterId");

                 entity.HasOne(d => d.OrganizationLevel)
                .WithMany(p => p.RevenuePlan)
                 .HasForeignKey(d => d.OrganizationLevelId)
                 .OnDelete(DeleteBehavior.ClientSetNull)
                 .HasConstraintName("FK_RevenuePlan_OrganizationLevelId");


                 entity.HasOne(d => d.Year)
                    .WithMany(p => p.RevenuePlan)
                    .HasForeignKey(d => d.YearId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RevenuePlan_Year");                 

             });
           
        }
    }
}


