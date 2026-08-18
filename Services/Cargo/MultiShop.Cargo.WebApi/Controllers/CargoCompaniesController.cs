using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.DtoLayer.Dtos.CargoCompanyDtos;
using MultiShop.Cargo.EntityLayer.Concrete;

namespace MultiShop.Cargo.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CargoCompaniesController : ControllerBase
    {
        private readonly ICargoCompanyService _cargoCompanyService;
        private readonly IMapper _mapper;

        public CargoCompaniesController(ICargoCompanyService cargoCompanyService, IMapper mapper)
        {
            _cargoCompanyService = cargoCompanyService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult CargoCompanyList()
        {
            var values = _mapper.Map<List<ResultCargoCompanyDto>>(_cargoCompanyService.TGetAll());
            return Ok(values);
        }

        [HttpGet("{id}")]
        public IActionResult GetCargoCompany(int id)
        {
            var value = _mapper.Map<GetByIdCargoCompanyDto>(_cargoCompanyService.TGetById(id));
            return Ok(value);
        }

        [HttpPost]
        public IActionResult CreateCargoCompany(CreateCargoCompanyDto createCargoCompanyDto)
        {
            _cargoCompanyService.TInsert(_mapper.Map<CargoCompany>(createCargoCompanyDto));
            return Ok("Başarılı");
        }

        [HttpPut]
        public IActionResult UpdateCargoCompany(UpdateCargoCompanyDto updateCargoCompanyDto)
        {
            _cargoCompanyService.TUpdate(_mapper.Map<CargoCompany>(updateCargoCompanyDto));
            return Ok("Başarılı");
        }

        [HttpDelete]
        public IActionResult DeleteCargoCompany(int id)
        {
            _cargoCompanyService.TDelete(id);
            return Ok("Başarılı");
        }
    }
}
