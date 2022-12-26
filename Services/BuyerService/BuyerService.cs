using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using E_Auction.Data;
using E_Auction.Dtos.Buyer;
using E_Auction.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Auction.Services.BuyerService
{
    public class BuyerService : IBuyerService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BuyerService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _context = context;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User
                                        .FindFirstValue(ClaimTypes.NameIdentifier));
        public async Task<ServiceResponse<List<GetBuyerDto>>> CreateBuyer(AddBuyerDto newBid)
        {
            ServiceResponse<List<GetBuyerDto>> response = new ServiceResponse<List<GetBuyerDto>>();

            if(BuyerNameValidation(newBid))
            {
                Buyer buyer = _mapper.Map<Buyer>(newBid);
                buyer.User = _context.Users
                    .FirstOrDefault(u => u.Id == GetUserId());
                buyer.UserId = buyer.User.Id;
                buyer.Product = _context.Products
                    .FirstOrDefault(p => p.Id == newBid.ProductId);
                if (buyer.Product != null)
                {
                    _context.Buyers.Add(buyer);
                    await _context.SaveChangesAsync();

                    response.Data = _context.Buyers
                        .Where(b => b.UserId == GetUserId())
                        .Select(b => _mapper.Map<GetBuyerDto>(b))
                        .ToList();
                }
                else
                { 
                    response.Success = false;
                    response.Message = "Product not found";
                }
            }
            else
            {
                response.Success = false;
                response.Message = "Product didnt pass the Name Validation";
            }


                

            return response;
        }

        private bool BuyerNameValidation(AddBuyerDto newBid)
        {
            bool flag = true;
            if(newBid.FirstName != string.Empty)                        
            {
                int length = newBid.FirstName.Length;
                if(length >= 5 && length <= 30)
                {
                    
                }
                else
                {
                    flag = false;
                    return flag;
                }
            }
            else
            {
                flag = false;
                return flag;
            }

            if(newBid.LastName != null)
            {
                int length = newBid.LastName.Length;
                if(length >= 3 && length <= 25)
                {

                }
                else
                {
                    flag = false;
                    return flag;
                }
            }
            else
            {
                flag = false;
                return flag;
            }

            if(newBid.Email != null)
            {
                string emailCheck = "@";

                if(newBid.Email.Contains(emailCheck))
                {

                }
                else
                {
                    flag = false;
                    return flag;
                }
            }
            else
            {
                flag = false;
                return flag;
            }

            if(newBid.Phone != null && newBid.Phone > 0 )
            {
                int phoneDigits = newBid.Phone.ToString().Length;
                if(phoneDigits == 10)
                {
                    return flag;
                }
                else
                {
                    flag = false;
                    return flag;
                }
            }
            else
            {
                flag = false;
                return flag;
            }
        }
    }
}