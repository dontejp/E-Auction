using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Auction.Dtos.Buyer;
using E_Auction.Models;

namespace E_Auction.Services.BuyerService
{
    public interface IBuyerService
    {
        Task<ServiceResponse<List<GetBuyerDto>>> CreateBuyer(AddBuyerDto newBid);       //placing a bid
        Task<List<GetBuyerDto>> GetBuyersByProductId(int id);                                      //getting all buyers

    }
}