using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using E_Auction.Data;
using E_Auction.Dtos.Product;
using E_Auction.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Auction.Services.ProductService
{
    public class ProductService : IProductService
    {
        //fields
        List<Product> _data;
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        //constructor
        public ProductService(IMapper mapper ,DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _context = context;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User
                                        .FindFirstValue(ClaimTypes.NameIdentifier));
        public async Task<ServiceResponse<List<GetProductDto>>> AddProduct(AddProductDto newProduct)        
        {

            ServiceResponse<List<GetProductDto>> response = new ServiceResponse<List<GetProductDto>>();         //Dto instantiation
            if(ProductNameValidation(newProduct))
            {
                Product product = _mapper.Map<Product>(newProduct);                                             //Mapping the DTO to the Product
                product.Seller = _context.Sellers
                    .FirstOrDefault(s => s.User.Id == GetUserId());
                product.SellerId = product.Seller.Id;

                _context.Products.Add(product);                                                                 //Adding product to the database
                await _context.SaveChangesAsync();                                                              //Saving changes in the database

                response.Data = await _context.Products                                                         //populating response ... grabbing from Products Table in the database
                    .Select(p => _mapper.Map<GetProductDto>(p))                                                 //Maps the products selected to Dto
                    .ToListAsync();                                                                             //puts it in a list form

            }
            else
            {
                response.Success = false;
                response.Message = "Product didnt pass the Name Validation";
            }

                return response;
        }
        public async Task<ServiceResponse<List<GetProductDto>>> DeleteProduct(int id)
        {
            ServiceResponse<List<GetProductDto>> response = new ServiceResponse<List<GetProductDto>>();         //Dto instantiation
            Product product = _context.Products
                .FirstOrDefault(p => p.Id == id);                                                               //Search Products table where the project id = id given and save it as a new product
            
            Seller seller = _context.Sellers
                .FirstOrDefault(s => s.UserId == GetUserId());

            if(product.SellerId == seller.UserId)
            {

                if(product != null) 
                {
                    if (DateTime.Compare(product.BidEndDate, DateTime.Now) > 0)                                          //if this new product isnt null AND the BidEndDate is later than now
                    {
                        if( product.Buyers == null )
                        {
                            _context.Products.Remove(product);                                                              //remove product from database
                            await _context.SaveChangesAsync();                                                              //save chanes in the database

                            response.Data = await _context.Products                                                         //populating response...grabbing all products from table
                                .Select(p => _mapper.Map<GetProductDto>(p))
                                .ToListAsync();
                        }
                        else
                        {
                            response.Success = false;
                            response.Message = "Cannot Delete! Buyers already have bids on this!";
                        }
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = "Cannot delete! The bid closed on: " + product.BidEndDate;
                    }
                }
                else
                {
                    response.Success = false;
                    response.Message = "Product with id: "+ id +" could not be found";
                }
            }
            else
            {
                response.Success = false;
                response.Message = "Cannot Delete! You do not own this product.";
            }

                return response;
        }
        public async Task<ServiceResponse<GetProductDto>> GetProductById(int id)
        {
            ServiceResponse<GetProductDto> response = new ServiceResponse<GetProductDto>();

            Product product = _context.Products.FirstOrDefault(p => p.Id == id);
            
            if(product != null)
            {
                response.Data = _mapper.Map<GetProductDto>(product);
            }
            else
            {
                response.Success = false;
                response.Message = "Product with id: '"+ id +"' could not be found";
            }

            return response;
        }
        public async Task<ServiceResponse<List<GetProductDto>>> GetProducts()
        {
            var dbproduct = await _context.Products.Select(p =>_mapper.Map<GetProductDto>(p)).ToListAsync();
            ServiceResponse<List<GetProductDto>> response = new ServiceResponse<List<GetProductDto>>();
            if(dbproduct != null)
            {
            response.Data = dbproduct;
            }
            else 
            {
                response.Success = false;
                response.Message = "Data not found";
            }
            return response;
        }


        /////////////////////////////////////////////////////////////////////
        //////////                                               ////////////
        /////////              **Validations**                  /////////////
        ////////                                              //////////////
        /////////////////////////////////////////////////////////////////////

        private bool ProductNameValidation(AddProductDto newProduct)        //checks if productName is not Null and between 5&30 
        {
            bool flag = true;
            if(newProduct.Name != string.Empty)
            {
                int length = newProduct.Name.Length;
                if(length >= 5 && length <= 30)
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