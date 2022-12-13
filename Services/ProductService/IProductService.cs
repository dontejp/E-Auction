using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Auction.Dtos.Product;
using E_Auction.Models;

namespace E_Auction.Services.ProductService
{
    public interface IProductService
    {
        Task<ServiceResponse<List<GetProductDto>>> GetProducts();
        Task<ServiceResponse<GetProductDto>> GetProductById(int id);
        Task<ServiceResponse<List<GetProductDto>>> AddProduct(AddProductDto newProduct);
        Task<ServiceResponse<List<GetProductDto>>> DeleteProduct(int id);
    }
}