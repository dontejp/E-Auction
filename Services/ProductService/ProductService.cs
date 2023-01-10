using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using E_Auction.Data;
using E_Auction.Dtos.Product;
using E_Auction.Dtos.Seller;
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
        // public async Task<List<GetProductDto>> AddProduct(AddProductDto newProduct)        
        // {

        //     List<GetProductDto> Data = new List<GetProductDto>();         //Dto instantiation
        //     if(ProductNameValidation(newProduct))
        //     {
        //         Product product = _mapper.Map<Product>(newProduct);                                             //Mapping the DTO to the Product
        //         product.Seller = _context.Sellers
        //             .FirstOrDefault(s => s.User.Id == GetUserId());
        //         product.SellerId = product.Seller.Id;

        //         _context.Products.Add(product);                                                                 //Adding product to the database
        //         await _context.SaveChangesAsync();                                                              //Saving changes in the database

        //         Data = await _context.Products                                                         //populating response ... grabbing from Products Table in the database
        //             .Select(p => _mapper.Map<GetProductDto>(p))                                                 //Maps the products selected to Dto
        //             .ToListAsync();                                                                             //puts it in a list form

        //     }

        //         return Data;
        // }

        public async Task<GetProductDto> AddProduct(AddProductWithSellerDto newProduct)    //This is the origional functionality of the assignment
        {
            GetProductDto Data = new GetProductDto();

            AddProductDto testName = new AddProductDto();

            testName.Name = newProduct.Name;
            testName.ShortDescription = newProduct.ShortDescription;
            testName.DetailedDescription = newProduct.DetailedDescription;
            testName.Category = newProduct.Category;
            testName.StartingPrice = newProduct.StartingPrice;
            testName.BidEndDate = newProduct.BidEndDate;

            if(ProductNameValidation(testName))
            {
                Product product = _mapper.Map<Product>(newProduct);

                product.Seller = _context.Sellers
                    .FirstOrDefault(s => s.Id == product.SellerId);

                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                product = await _context.Products.FirstOrDefaultAsync(p => p.Name == newProduct.Name);

                Data = _mapper.Map<GetProductDto>(product);
                
            }

                return Data;
        }
        public async Task<List<GetProductDto>> DeleteProduct(int id)
        {
            List<GetProductDto> Data = new List<GetProductDto>();         //Dto instantiation
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

                            Data = await _context.Products                                                         //populating response...grabbing all products from table
                                .Select(p => _mapper.Map<GetProductDto>(p))
                                .ToListAsync();
                        }
                        else
                        {
                            // response.Success = false;
                            // response.Message = "Cannot Delete! Buyers already have bids on this!";
                        }
                    }
                    else
                    {
                        // response.Success = false;
                        // response.Message = "Cannot delete! The bid closed on: " + product.BidEndDate;
                    }
                }
                else
                {
                    // response.Success = false;
                    // response.Message = "Product with id: "+ id +" could not be found";
                }
            }
            else
            {
                // response.Success = false;
                // response.Message = "Cannot Delete! You do not own this product.";
            }

                return Data;
        }
        public async Task<GetProductDto> GetProductById(int id)
        {
            GetProductDto Data = new GetProductDto();

            Product product = _context.Products.FirstOrDefault(p => p.Id == id);
            
            if(product != null)
            {
                Data = _mapper.Map<GetProductDto>(product);
            }
            else
            {
                // response.Success = false;
                // response.Message = "Product with id: '"+ id +"' could not be found";
            }

            return Data;
        }
        public async Task<List<GetProductDto>> GetProducts()
        {
            var dbproduct = await _context.Products.Select(p =>_mapper.Map<GetProductDto>(p)).ToListAsync();
            List<GetProductDto> Data = new List<GetProductDto>();
            if(dbproduct != null)
            {
            Data = dbproduct;
            }
            else 
            {
                // response.Success = false;
                // response.Message = "Data not found";
            }
            return Data;
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