using AutoMapper;
using Microsoft.AspNetCore.Http;
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

        /// <summary>
        /// Get all Products 
        /// </summary>
        /// <returns>Products list, each product has id, name, price, imageUrl and companyId fields</returns>
        /// <response code="200">(Success) Returns a list of Products</response>
        //GET:api/products
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetProductViewModel))]
        [HttpGet()]
        public IActionResult GetProducts()
        {
            var productsFromRepo = _unitOfWork.Products.GetProductsWithCompany();
            var products = Mapper.Map<IEnumerable<GetProductViewModel>>(productsFromRepo);
            return Ok(products);

        }

        //GET:api/products/5
        /// <summary>
        /// Get Product by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A Product with id, name, price, imageUrl and companyId fields</returns>
        /// <response code="200">(Success) Returns a single Product</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetProductViewModel))]
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
        /// <summary>
        /// Updates a Product by id and providing modified Product as an object 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
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
            //if user tries to update product with company that doesn't exist
            if (_unitOfWork.Companies.Get(product.CompanyId) == null)
            {
                return NotFound();
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
