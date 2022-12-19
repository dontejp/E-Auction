using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using E_Auction.Data;

namespace E_Auction.Dtos.Buyer
{
    public class GetBuyerDto
    {
        public string FirstName { get; set; }
        public int Phone { get; set; }
        public string Email { get; set; }
        public int BidAmount { get; set; }
    }
}