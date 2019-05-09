using AutoMapper;
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
        [HttpGet()]
        public IActionResult GetCompanies()
        {
            var companiesFromRepo = _unitOfWork.Companies.GetAll();
            var companies = Mapper.Map<IEnumerable<GetCompanyViewModel>>(companiesFromRepo);
            return Ok(companies);

        }

        //GET:api/products/5
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
