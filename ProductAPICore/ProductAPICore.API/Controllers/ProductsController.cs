using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductAPICore.API.ViewModels;
using ProductAPICore.Model.Core;
using System.Collections.Generic;

namespace ProductAPICore.API.Controllers
{
    [Route("api/products")]
    public class ProductsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet()]
        public IActionResult GetProducts()
        {
            var productsFromRepo = _unitOfWork.Products.GetProductsWithCompany();
            var products = Mapper.Map<IEnumerable<ProductViewModel>>(productsFromRepo);
            return Ok(products);

        }
        [HttpGet("{id}", Name = "GetProductById")]
        public IActionResult GetProduct(int id)
        {
            var productFromRepo = _unitOfWork.Products.GetProductWithCompany(id);
            if (productFromRepo == null)
            {
                return NotFound();
            }
            var product = Mapper.Map<ProductViewModel>(productFromRepo);
            return Ok(product);

        }
    }
}
