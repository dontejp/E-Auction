using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Auction.Models
{
    public class Product
    {
        public int Id {get; set;}
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string DetailedDescription { get; set; }
        public Category Category { get; set; }
        public double StartingPrice { get; set; }
        public DateTime BidEndDate {get;set;}
        public Seller Seller { get; set; }
        public int SellerId { get; set; }
        public List<Buyer> Buyers { get; set; }
    }
}