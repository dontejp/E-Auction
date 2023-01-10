using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Auction.Dtos.Seller;
using E_Auction.Models;

namespace E_Auction.Services.SellerService
{
    public interface ISellerService
    {
        Task<ServiceResponse<GetSellerDto>> AddSeller(AddSellerDto newSeller);
        Task<GetSellerDto> GetSellerById(int id);

    }
}