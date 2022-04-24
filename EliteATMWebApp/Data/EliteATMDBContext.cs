using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using EliteATMWebApp.Models;

namespace EliteATMWebApp.Data
{
    public partial class EliteATMDBContext : DbContext
    {
        public EliteATMDBContext()
        {
        }

        public EliteATMDBContext(DbContextOptions<EliteATMDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("workstation id=eliteatmdatabase.mssql.somee.com;packet size=4096;user id=Svengali_SQLLogin_1;pwd=tocx9tglx2;data source=eliteatmdatabase.mssql.somee.com;persist security info=False;initial catalog=eliteatmdatabase");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasOne(d => d.User)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Accounts_Users");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
