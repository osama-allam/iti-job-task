using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ProductAPICore.API.ViewModels;
using ProductAPICore.Model.Core;
using ProductAPICore.Model.Helpers;
using System;
using System.Collections.Generic;

namespace ProductAPICore.API.Controllers
{
    [Route("api/products")]
    public class ProductsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly LinkGenerator _linkGenerator;

        public ProductsController(IUnitOfWork unitOfWork, LinkGenerator linkGenerator)
        {
            _unitOfWork = unitOfWork;
            _linkGenerator = linkGenerator;
        }


        /// <summary>
        /// Get all Products with pagination
        /// </summary>
        /// <param name="productsResourceParameters">ProductsResourceParameters this object has two members PageNumber and PageSize</param>
        /// <returns>Products list, each product has id, name, price, imageUrl and companyId fields</returns>
        /// <response code="200">(Success) Returns a list of Products</response>
        /// <response code="406">(Not Acceptable) In case of using setting response other than JSON or XML</response>
        //GET:api/products
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetProductViewModel))]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        [HttpGet(Name = "GetProducts")]
        public IActionResult GetProducts(ProductsResourceParameters productsResourceParameters)
        {
            var productsFromRepo = _unitOfWork.Products.GetProductsWithCompany(productsResourceParameters);
            var previousPageLink = productsFromRepo.HasPrevious
                ? CreateProductsPaginationUri(productsResourceParameters, PageUriType.PreviousPage)
                : null;
            var nextPageLink = productsFromRepo.HasNext
                ? CreateProductsPaginationUri(productsResourceParameters, PageUriType.NextPage)
                : null;
            var paginationMetaData = new
            {
                totalCount = productsFromRepo.TotalCount,
                pageSize = productsFromRepo.PageSize,
                currentPage = productsFromRepo.CurrentPage,
                totalPages = productsFromRepo.TotalPages,
                previousPageLink = previousPageLink,
                nextPageLink = nextPageLink
            };
            Response.Headers.Add("X-Pagination", Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetaData));

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
        /// <response code="406">(Not Acceptable) In case of using setting response other than JSON or XML</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetProductViewModel))]
        [HttpGet("{id}", Name = "GetProduct")]
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
        /// <response code="204">(Success) Product updated successfully</response>
        /// <response code="406">(Not Acceptable) In case of using setting response other than JSON or XML</response>
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        //uncomment below line if you want to accept only JSON objects
        //[Consumes("application/json")]
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

        private string CreateProductsPaginationUri(ProductsResourceParameters productsResourceParameters,
            PageUriType pageUriType)
        {
            switch (pageUriType)
            {
                case PageUriType.PreviousPage:


                    return _linkGenerator.GetUriByAction(HttpContext, "GetProducts",
                        values: new
                        {
                            pageNumber = productsResourceParameters.PageNumber - 1,
                            pageSize = productsResourceParameters.PageSize
                        });
                    break;
                case PageUriType.NextPage:
                    return _linkGenerator.GetUriByAction(HttpContext, "GetProducts",
                        values: new
                        {
                            pageNumber = productsResourceParameters.PageNumber + 1,
                            pageSize = productsResourceParameters.PageSize
                        });

                    //return _urlHelper.Link("GetProducts",
                    //    new
                    //    {
                    //        pageNumber = productsResourceParameters.PageNumber + 1,
                    //        pageSize = productsResourceParameters.PageSize
                    //    });
                    break;
                default:

                    return _linkGenerator.GetUriByAction(HttpContext, "GetProducts",
                        values: new
                        {
                            pageNumber = productsResourceParameters.PageNumber + 1,
                            pageSize = productsResourceParameters.PageSize
                        });
                    break;
            }
        }
    }
}
