﻿using Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class DataContext : DbContext
    { 
        public DataContext() { }
        public DataContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlServer("Server=localhost\\MSSQLSERVER02;Database=IcmOdivelasDb;Trusted_Connection=True;encrypt=false;");
            }
        }

        public DbSet<Member> Members { get; set; }
        public DbSet<Function> Functions { get; set; }
        public DbSet<MemberFunction> MemberFunctions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Situation> Situations { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MemberFunction>()
             .HasKey(mf => new { mf.MemberId, mf.FunctionId });

            modelBuilder.Entity<Member>()
            .HasMany(e => e.Functions)
            .WithMany(e => e.Members)
            .UsingEntity<MemberFunction>();

            modelBuilder.Entity<Function>()
              .HasMany(e => e.Members)
              .WithMany(e => e.Functions)
              .UsingEntity<MemberFunction>();

            modelBuilder.Entity<Member>()
                .HasOne(m => m.Category)
                .WithMany(c => c.Members)
                .HasForeignKey(m => m.CategoryId);

            modelBuilder.Entity<Member>()
                .HasOne(m => m.Group)
                .WithMany(g => g.Members)
                .HasForeignKey(m => m.GroupId);

            modelBuilder.Entity<Member>()
                .HasOne(m => m.Situation)
                .WithMany(s => s.Members)
                .HasForeignKey(m => m.SituationId);

        }
    }

}
