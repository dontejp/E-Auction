using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Auction.Models;

namespace E_Auction.Dtos.Product
{
    public class GetProductDto
    {
        public int Id {get; set;}
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string DetailedDescription { get; set; }
        public Category Category { get; set; }
        public double StartingPrice { get; set; }
        public DateTime BidEndDate {get;set;}
        public int SellerId { get; set; }
    }
}