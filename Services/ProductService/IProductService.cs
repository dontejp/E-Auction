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
        Task<List<GetProductDto>> GetProducts();
        Task<GetProductDto> GetProductById(int id);
        Task<GetProductDto> AddProduct(AddProductWithSellerDto newProductWithSeller);
        Task<List<GetProductDto>> DeleteProduct(int id);
    }
}