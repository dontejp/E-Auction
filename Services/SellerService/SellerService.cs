using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using E_Auction.Data;
using E_Auction.Dtos.Seller;
using E_Auction.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Auction.Services.SellerService
{
    public class SellerService : ISellerService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public SellerService(DataContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _context = context;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User
                                        .FindFirstValue(ClaimTypes.NameIdentifier));
        public async Task<ServiceResponse<GetSellerDto>> AddSeller(AddSellerDto newSeller)
        {
            ServiceResponse<GetSellerDto> response = new ServiceResponse<GetSellerDto>();

            if(SellerNameValidation(newSeller))
            {
                Seller seller = _mapper.Map<Seller>(newSeller);
                seller.User = await _context.Users
                    .FirstOrDefaultAsync(u => u.Id == GetUserId());

                _context.Sellers.Add(seller);
                await _context.SaveChangesAsync();

                Seller dbSeller = await _context.Sellers
                            .FirstOrDefaultAsync(s => s.User.Id == GetUserId());
                
                response.Data = _mapper.Map<GetSellerDto>(dbSeller);

            }
            else
            {
                response.Success = false;
                response.Message = "Product didnt pass the Name Validation";
            }
            return response;
        }

        public Task<ServiceResponse<GetSellerDto>> GetSellerById()
        {
            throw new NotImplementedException();
        }

        private bool SellerNameValidation(AddSellerDto newSeller)
        {
            bool flag = true; 

            if(newSeller.FirstName != null)
            {
                int length = newSeller.FirstName.Length;

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

            if(newSeller.LastName != null)
            {
                int length = newSeller.LastName.Length;

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

            if(newSeller.Email != null)
            {
                string emailCheck = "@";

                if(newSeller.Email.Contains(emailCheck))
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

            if(newSeller.Phone != null && newSeller.Phone > 0 )
            {
                int phoneDigits = newSeller.Phone.ToString().Length;
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