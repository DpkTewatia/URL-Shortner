using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using UrlShorter.Models;

namespace UrlShorter.Date
{
    public class AppDbContext : DbContext
    {
        public DbSet<UrlTable> UrlsTabl { get; set; }
        public DbSet<UserTable> UsersTabl { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Inforce\\URL-Shortner\\Db.mdf;Integrated Security=True;Connect Timeout=30");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);

           
            modelBuilder.Entity<UrlTable>()
                .HasOne(u => u.User)
                .WithMany()
                .HasForeignKey(u => u.UserId);
        }

    }
}
