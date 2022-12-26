using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using E_Auction.Dtos.Buyer;
using E_Auction.Dtos.Product;
using E_Auction.Dtos.Seller;
using E_Auction.Models;

namespace E_Auction
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AddProductDto, Product>();
            CreateMap<Product, GetProductDto>();
            CreateMap<AddSellerDto, Seller>();
            CreateMap<Seller, GetSellerDto>();
            CreateMap<AddBuyerDto, Buyer>();
            CreateMap<Buyer, GetBuyerDto>();
        }
    }
}