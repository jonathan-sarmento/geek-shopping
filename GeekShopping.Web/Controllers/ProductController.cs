using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeekShopping.Web.Models;
using GeekShopping.Web.Services.Abstractions;
using GeekShopping.Web.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ProductIndex()
        {
            var products = await _productService.FindAllProductsAsync();
            return View(products);
        }
        
        [HttpGet]
        public async Task<IActionResult> ProductCreate()
        {
            return View();
        }
        
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ProductCreate(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.CreateProductAsync(model);
                if (response != default) return RedirectToAction(nameof(ProductIndex));
            }
            return View(model);
        }
        
        public async Task<IActionResult> ProductUpdate(int id)
        {
            var model = await _productService.FindProductByIdAsync(id);
            if (model != null) return View(model);

            return NotFound();
        }
        
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ProductUpdate(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.UpdateProductAsync(model);
                if (response != default) return RedirectToAction(nameof(ProductIndex));
            }
            return View(model);
        }
        
        [Authorize]
        public async Task<IActionResult> ProductDelete(int id)
        {
            var model = await _productService.FindProductByIdAsync(id);
            if (model != null) return View(model);

            return NotFound();
        }
        
        [HttpPost]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> ProductDelete(ProductModel model)
        {
            var response = await _productService.DeleteProductByIdAsync(model.Id);
            if (response != default) return RedirectToAction(nameof(ProductIndex));
            
            return View(model);
        }
    }
}