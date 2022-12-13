using System;
using System.Collections.Generic;
using System.Linq;
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

        //constructor
        public ProductService(IMapper mapper ,DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<List<GetProductDto>>> AddProduct(AddProductDto newProduct)        
        {

            ServiceResponse<List<GetProductDto>> response = new ServiceResponse<List<GetProductDto>>();                                           //Dto instantiation
            if(await ProductNameValidation(newProduct))
            {
                Product product = _mapper.Map<Product>(newProduct);                                             //Mapping the DTO to the Product

                _context.Products.Add(product);                                                                 //Adding product to the database
                await _context.SaveChangesAsync();                                                              //Saving changes in the database

                response.Data = await _context.Products                                                              //populating response ... grabbing from Products Table in the database
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
            ServiceResponse<List<GetProductDto>> response = new ServiceResponse<List<GetProductDto>>();
            Product product = _context.Products
                .FirstOrDefault(p => p.Id == id);
            
            if(product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();

                response.Data = await _context.Products
                    .Select(p => _mapper.Map<GetProductDto>(p))
                    .ToListAsync();
            
            }
            else
            {
                response.Success = false;
                response.Message = "Product with id: "+ id +" could not be found";
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

        private async Task<bool> ProductNameValidation(AddProductDto newProduct)        //checks if productName is not Null and between 5&30 
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