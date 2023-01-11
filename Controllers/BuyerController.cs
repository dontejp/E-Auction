using E_Auction.Dtos.Buyer;
using E_Auction.Models;
using E_Auction.Services.BuyerService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Auction.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class BuyerController : ControllerBase
    {
        private readonly IBuyerService _buyerService;
        public BuyerController(IBuyerService buyerService)
        {
            _buyerService = buyerService;
        }

        [HttpPost]

        public async Task<ActionResult<ServiceResponse<List<GetBuyerDto>>>> AddBuyer(AddBuyerDto newBid)
        {
            return Ok(await _buyerService.CreateBuyer(newBid));
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<List<GetBuyerDto>>> GetBuyersByProductId (int id)
        {
            return Ok(await _buyerService.GetBuyersByProductId(id));
        }

    }
}
