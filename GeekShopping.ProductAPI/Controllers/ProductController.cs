using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GeekShopping.ProductAPI.Domain.ValueObjects;
using GeekShopping.ProductAPI.Infrastructure.Abstractions;
using GeekShopping.ProductAPI.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.ProductAPI.Controllers
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
        [Authorize]
        public async Task<ActionResult<Product>> FindById(long id, CancellationToken cancellationToken)
        {
            var product = await _repository.SelectByIdAsync(id, cancellationToken);
            
            return product == null ? NotFound() : Ok(product);
        }
        
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            var products = await _repository.GetAllAsync();
            
            return Ok(products);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<long>> Create([FromBody] Product product, CancellationToken cancellationToken)
        {
            if (product == null) return BadRequest();
            
            await _repository.AddAsync(product, cancellationToken);
            return Ok(product.Id);
        }
        
        [HttpPut]
        [Authorize]
        public async Task<ActionResult<Product>> Update([FromBody] Product product, CancellationToken cancellationToken)
        {
            if (product == null) 
                return BadRequest();
            
            await _repository.UpdateAsync(product, cancellationToken);
            return Ok(product);
        }
        
        [HttpDelete("{id}")]
        [Authorize(Roles = Role.Admin)]
        public async Task<ActionResult<bool>> Delete(long id, CancellationToken cancellationToken)
        {
            if (id == 0) 
                return BadRequest();
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