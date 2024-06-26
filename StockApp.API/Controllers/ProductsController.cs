﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockApp.Application.DTOs;
using StockApp.Domain.Entities;
using StockApp.Domain.Interfaces;

namespace tp2_stockapp_ava.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet(Name = "GetProducts")]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            var products = await _productRepository.GetProducts();
            if (products == null)
            {
                return NotFound("Products not found");
            }
            return Ok(products);
        }

        [HttpGet("{id:int}", Name = "GetProduct")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            var product = await _productRepository.GetById(id);
            if (product == null)
            {
                return NotFound("Product not Found");
            }
            return Ok(product);
        }

        [HttpPost(Name = "CreateProduct")]
        public async Task<ActionResult<Product>> Create(Product product)
        {
            if (product == null)
            {
                return BadRequest("Invalid Data");
            }
            await _productRepository.Create(product);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        [HttpPut(Name = "UpdateCategory")]
        public async Task<IActionResult> Update(int id, Product product)
        {
            if (product == null)
            {
                return BadRequest("Update Data Invalid");
            }

            if (id != product.Id)
            {
                return BadRequest();
            }
            await _productRepository.Update(product);
            return NoContent();
        }

        [HttpDelete("{id:int}", Name = "DeleteProduct")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productRepository.GetById(id);
            if (product == null)
            {
                return NotFound("Product not found");
            }
            await _productRepository.Remove(product);
            return NoContent();
        }
    }
}