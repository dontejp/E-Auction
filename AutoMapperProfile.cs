using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using E_Auction.Dtos.Product;
using E_Auction.Dtos.Seller;
using E_Auction.Models;

namespace E_Auction
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, GetProductDto>();
            CreateMap<AddProductDto, Product>();
            CreateMap<AddSellerDto, Seller>();
            CreateMap<Seller, GetSellerDto>();
        }
    }
}