using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GeekShopping.ProductAPI.Domain.ValueObjects;
using GeekShopping.ProductAPI.Infrastructure.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.ProductAPI.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _repository;
        
        public ProductController(IProductRepository repository)
        {
            _repository = repository;
        }
        
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Product>> FindById(long id, CancellationToken cancellationToken)
        {
            var product = await _repository.SelectByIdAsync(id, cancellationToken);
            
            return product == null ? NotFound() : Ok(product);
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            var products = await _repository.GetAllAsync();
            
            return Ok(products);
        }

        [HttpPost]
        public async Task<ActionResult<long>> Create([FromBody] Product product, CancellationToken cancellationToken)
        {
            if (product == null) return BadRequest();
            
            await _repository.AddAsync(product, cancellationToken);
            return Ok(product.Id);
        }
        
        [HttpPut]
        public async Task<ActionResult<Product>> Update(Product product, CancellationToken cancellationToken)
        {
            if (product == null) 
                return BadRequest();
            
            await _repository.UpdateAsync(product, cancellationToken);
            return Ok(product.Id);
        }
        
        [HttpDelete("id")]
        public async Task<ActionResult> Delete(long id, CancellationToken cancellationToken)
        {
            if (id == 0) 
                BadRequest();
            try
            {
                await _repository.DeleteAsync(id, cancellationToken);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
            return Ok(true);
        }
    }
}