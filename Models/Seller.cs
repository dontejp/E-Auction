using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Auction.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Pin { get; set; }
        public int Phone { get; set; }
        public string Email { get; set; }
        public List<Product>? Products { get; set; }
        public User? User { get; set; }
        public int UserId { get; set; }
    }
}