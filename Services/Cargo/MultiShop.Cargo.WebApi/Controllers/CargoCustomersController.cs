using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.DtoLayer.Dtos.CargoCustomerDtos;
using MultiShop.Cargo.EntityLayer.Concrete;

namespace MultiShop.Cargo.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CargoCustomersController : ControllerBase
    {
        private readonly ICargoCustomerService _cargoCustomerService;
        private readonly IMapper _mapper;

        public CargoCustomersController(ICargoCustomerService cargoCustomerService, IMapper mapper)
        {
            _cargoCustomerService = cargoCustomerService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult CargoCustomerList()
        {
            var values = _mapper.Map<List<ResultCargoCustomerDto>>(_cargoCustomerService.TGetAll());
            return Ok(values);
        }

        [HttpGet("{id}")]
        public IActionResult GetCargoCustomer(int id)
        {
            var value = _mapper.Map<GetByIdCargoCustomerDto>(_cargoCustomerService.TGetById(id));
            return Ok(value);
        }

        [HttpPost]
        public IActionResult CreateCargoCustomer(CreateCargoCustomerDto createCargoCustomerDto)
        {
            _cargoCustomerService.TInsert(_mapper.Map<CargoCustomer>(createCargoCustomerDto));
            return Ok("Başarılı");
        }

        [HttpPut]
        public IActionResult UpdateCargoCustomer(UpdateCargoCustomerDto updateCargoCustomerDto)
        {
            _cargoCustomerService.TUpdate(_mapper.Map<CargoCustomer>(updateCargoCustomerDto));
            return Ok("Başarılı");
        }

        [HttpDelete]
        public IActionResult DeleteCargoCustomer(int id)
        {
            _cargoCustomerService.TDelete(id);
            return Ok("Başarılı");
        }

        [HttpGet("GetCargoCustomerById")]
        public async Task<IActionResult> GetCargoCustomerById(string id)
        {
            var value = await _cargoCustomerService.TGetCargoCustomerByIdAsync(id);
            return Ok(value);
        }
    }
}
