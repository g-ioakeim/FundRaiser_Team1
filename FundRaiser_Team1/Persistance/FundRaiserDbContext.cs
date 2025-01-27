﻿using Microsoft.EntityFrameworkCore;

namespace FundRaiser_Team1.Models
{
    public class FundRaiserDbContext: DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Package> Package { get; set; }
        public DbSet<ProjectUser> ProjectUser { get; set; }
        public DbSet<PackageUser> PackageUser { get; set; }

       /* public FundRaiserDbContext(DbContextOptions<FundRaiserDbContext> options) : base()
        {

        }*/

        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=FundRaiserDB;User= sa; password= admin!@#123");
            // optionsBuilder.UseSqlServer("Data Source=VASILISCHATZIS-\\SQLSERVER2019;Initial Catalog=FundRaiserDB;Integrated Security=True");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>()
            .HasMany(a => a.Projects)
            .WithMany(p => p.Users)
            .UsingEntity<ProjectUser>(
            ap => ap.HasOne<Project>().WithMany(),
            ap => ap.HasOne<User>().WithMany()
            )
            .Property(ap => ap.CategoryProject);

            modelBuilder.Entity<User>()
             .HasMany(a => a.Packages)
             .WithMany(p => p.Users)
             .UsingEntity<PackageUser>(
             ap => ap.HasOne<Package>().WithMany(),
             ap => ap.HasOne<User>().WithMany()
             );


            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Package>().ToTable("Package");
            modelBuilder.Entity<PackageUser>().ToTable("PackageUser");

            modelBuilder.Entity<User>()
                .HasIndex(user => user.Email)
                .IsUnique();

            

        }

    }
}
