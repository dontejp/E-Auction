using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Auction.Models;
using Microsoft.EntityFrameworkCore;


namespace E_Auction.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) :base(options)
        {
            
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Product> Buyers { get; set; }
        public DbSet<Product> Users { get; set; }
    }
}