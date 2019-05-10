using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductAPICore.API.ViewModels;
using ProductAPICore.Model.Core;
using System.Collections.Generic;

namespace ProductAPICore.API.Controllers
{

    [Route("api/companies")]
    public class CompaniesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CompaniesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //GET:api/products
        /// <summary>
        /// Get all Companies 
        /// </summary>
        /// <returns>Returns a list of companies</returns>
        /// <response code="200">(Success) Returns a list of Companies</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetCompanyViewModel))]
        [HttpGet()]
        public IActionResult GetCompanies()
        {
            var companiesFromRepo = _unitOfWork.Companies.GetAll();
            var companies = Mapper.Map<IEnumerable<GetCompanyViewModel>>(companiesFromRepo);
            return Ok(companies);

        }

        //GET:api/products/5
        /// <summary>
        /// Get company by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A Company with id and name fields</returns>
        /// <response code="200">(Success) Returns a single Company</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetCompanyViewModel))]
        [HttpGet("{id}")]
        public IActionResult GetCompany(int id)
        {
            var companyFromRepo = _unitOfWork.Companies.Get(id);
            if (companyFromRepo == null)
            {
                return NotFound();
            }
            var company = Mapper.Map<GetCompanyViewModel>(companyFromRepo);
            return Ok(company);

        }

    }
}
