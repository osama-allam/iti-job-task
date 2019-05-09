using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductAPICore.API.ViewModels;
using ProductAPICore.Model.Core;
using System;
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


        //GET:api/products
        [HttpGet()]
        public IActionResult GetProducts()
        {
            var productsFromRepo = _unitOfWork.Products.GetProductsWithCompany();
            var products = Mapper.Map<IEnumerable<GetProductViewModel>>(productsFromRepo);
            return Ok(products);

        }

        //GET:api/products/5
        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var productFromRepo = _unitOfWork.Products.GetProductWithCompany(id);
            if (productFromRepo == null)
            {
                return NotFound();
            }
            var product = Mapper.Map<GetProductViewModel>(productFromRepo);
            return Ok(product);

        }

        //PUT:api/products/5
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] UpdateProductViewModel product)
        {
            //if user made request with no data (check on product object)
            if (product == null)
            {
                return BadRequest();
            }

            //must be a valid form
            if (!ModelState.IsValid)
            {
                return new UnprocessableEntityObjectResult(ModelState);
            }

            //try to get this product with provided id (check on id)
            var productFromRepo = _unitOfWork.Products.Get(id);
            if (productFromRepo == null)
            {
                return NotFound();
            }
            // copy data from provided product into selected product
            Mapper.Map(product, productFromRepo);
            try
            {
                _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                throw new Exception($"Updating product of {id} failed to save.");
            }

            return NoContent();
        }
    }
}
