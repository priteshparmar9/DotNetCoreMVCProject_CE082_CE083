using BookBob_Bootstrap.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookBob_Bootstrap.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }
       //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      //{
        //  optionsBuilder.UseSqlServer("server=(localdb)\\MSSQLLocalDB;database=BookBob;Trusted_Connection=true");
        //}

        

        public DbSet<User> Users{ get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartItem>().HasKey(ci => new {ci.CartId, ci.BookId });
            modelBuilder.Entity<CartItem>().
                HasOne(ci => ci.Cart).WithMany(ci => ci.CartItems).HasForeignKey(ci => ci.CartId);
            modelBuilder.Entity<CartItem>().
                HasOne(ci => ci.Book).WithMany(ci => ci.CartItems).HasForeignKey(ci => ci.BookId);

        }
    }
}
