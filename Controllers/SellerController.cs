using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Auction.Dtos.Seller;
using E_Auction.Models;
using E_Auction.Services.SellerService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Auction.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class SellerController : ControllerBase
    {
        private readonly ISellerService _sellerService;
        public SellerController(ISellerService sellerService)
        {
            _sellerService = sellerService;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetSellerDto>>> AddSeller(AddSellerDto newSeller)
        {
            return Ok(await _sellerService.AddSeller(newSeller)); 
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetSellerDto>> GetSellerbyId(int id)
        {
            return Ok(await _sellerService.GetSellerById(id));
        }
    }
}