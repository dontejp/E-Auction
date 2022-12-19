using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using E_Auction.Data;
using E_Auction.Dtos.Buyer;
using E_Auction.Models;

namespace E_Auction.Services.BuyerService
{
    public class BuyerService 
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        public BuyerService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        // public Task<ServiceResponse<GetBuyerDto>> CreateBuyer()
        // {
        //     ServiceResponse<GetBuyerDto> response = new ServiceResponse<GetBuyerDto>();

        //     return response;
        // }
    }
}