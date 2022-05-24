using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PumoxRESTful.DAL;
using PumoxRESTful.Dtos;
using PumoxRESTful.Models;
using PumoxRESTful.Repositories;
using Newtonsoft.Json;


namespace PumoxRESTful.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = SchemesNamesConst.TokenAuthenticationDefaultScheme)]
    public class CompanyController : ControllerBase
    {
        private readonly ApplicationContext _db;
        private readonly ICompaniesRepository _companiesRepository;

        public CompanyController(ApplicationContext db, ICompaniesRepository repository)
        {
            this._db = db;
            this._companiesRepository = repository;
        }

        [BasicAuthorize]
        [HttpGet("{id}")]
        public JsonResult GetCompany(long id)
        {
            var company = _companiesRepository.GetCompany(id);

            if (company is null)
            {
                return new JsonResult(NotFound());
            }

            return new JsonResult((CompanyDto)company.AsDto());
        }

        [BasicAuthorize]
        [HttpPost("create")]
        public JsonResult Create([FromBody]CreateCompanyDto companyDto)
        {
            List<Employee> employees = new List<Employee>();

            Company company = new()
            {
                Name = companyDto.Name,
                EstablishmentYear = companyDto.EstablishmentYear,
            };

            foreach (var item in companyDto.Employees)
            {
                Employee employee = new()
                {
                    Company = company,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    DateOfBirth = item.DateOfBirth,
                    JobTitle = item.JobTitle
                };

                employees.Add(employee);
            }

            company.Employees = employees;
            var id = _companiesRepository.CreateCompany(company);

            CreateCompanyResultDto result = new()
            {
                Id = id
            };

            return new JsonResult(result);
        }

        [BasicAuthorize]
        [HttpPut("update/{id}")]
        public ActionResult UpdateCompany(long id, UpdateCompanyDto companyDto)
        {
            var existingCompany = _companiesRepository.GetCompany(id);

            if (existingCompany is null)
            {
                return NotFound();
            }

            existingCompany.Name = companyDto.Name;
            existingCompany.EstablishmentYear = companyDto.EstablishmentYear;
            List<Employee> companyEmployees = new List<Employee>();

            foreach (var item in companyDto.Employees)
            {
                Employee employee = new()
                {
                    Company = existingCompany,
                    DateOfBirth = item.DateOfBirth,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    JobTitle = item.JobTitle
                };

                companyEmployees.Add(employee);
            }

            existingCompany.Employees = companyEmployees;
            _companiesRepository.UpdateCompany(existingCompany);

            return NoContent();
        }

        [BasicAuthorize]
        [HttpPost("search")]
        public JsonResult SearchCompany([FromBody]SearchCompanyDto searchCompanyDto)
        {
            var searchCompanies = _companiesRepository.searchCompanies(searchCompanyDto.Keyword,
                searchCompanyDto.EmployeeDateOfBirthFrom, searchCompanyDto.EmployeeDateOfBirthTo,
                searchCompanyDto.EmployeeJobTitles);

            List<CompanyDto> result = new List<CompanyDto>();

            foreach (var resultItem in searchCompanies)
            {
                result.Add(resultItem.AsDto());
            }

            return new JsonResult(result);
        }

        [BasicAuthorize]
        [HttpDelete("delete/{id}")]
        public ActionResult DeleteCompany(long id)
        {
            var existingCompany = _companiesRepository.GetCompany(id);

            if (existingCompany is null)
            {
                return NotFound();
            }

            _companiesRepository.DeleteCompany(id);

            return NoContent();
        }
    }
}
