using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Auction.Data;
using E_Auction.Models;

namespace E_Auction.Services.ProductService
{
    public class ProductService : IProductService
    {
        List<Product> _data;
        private readonly DataContext _context;
        public ProductService(DataContext context)
        {
            _context = context;
        }
        public async Task<List<Product>> AddProduct()
        {
            throw new NotImplementedException();
        }
        public async Task<List<Product>> DeleteProduct(int id)
        {
            Product product = _data.FirstOrDefault(p => p.Id == id);
            if(product != null)
            {
                _data.Remove(product);
            }

            return _data;
        }
        public async Task<Product> GetProductById(int id)
        {
            Product product = _data.FirstOrDefault(p => p.Id == id);

            return product;
        }
        public async Task<List<Product>> GetProducts()
        {
            return _data;
        }
    }
}