using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Auction.Dtos.Product;
using E_Auction.Models;
using E_Auction.Services.ProductService;
using Microsoft.AspNetCore.Mvc;

namespace E_Auction.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetProductDto>>>> GetProducts()                             
        {
            return Ok(await _productService.GetProducts());                                     
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<ServiceResponse<GetProductDto>>> GetProductbyId(int id)                          
        {
            return Ok(await _productService.GetProductById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetProductDto>>>> AddProduct(AddProductDto newProduct)      
        {
            return Ok(await _productService.AddProduct(newProduct));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetProductDto>>>> DeleteProduct(int id)
        {
            return Ok(await _productService.DeleteProduct(id));
        }
    }
}