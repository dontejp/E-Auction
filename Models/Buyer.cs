using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Auction.Models
{
    public class Buyer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Pin { get; set; }
        public Int64 Phone { get; set; }
        public string Email { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; } 
        public int BidAmount { get; set; }
        public User? User { get; set; }
        public int UserId { get; set; }
    }
}