using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.DtoLayer.Dtos.CargoOperationDtos;
using MultiShop.Cargo.EntityLayer.Concrete;

namespace MultiShop.Cargo.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CargoOperationsController : ControllerBase
    {
        private readonly ICargoOperationService _cargoOperationService;
        private readonly IMapper _mapper;

        public CargoOperationsController(ICargoOperationService cargoOperationService, IMapper mapper)
        {
            _cargoOperationService = cargoOperationService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult CargoOperationList()
        {
            var values = _mapper.Map<List<ResultCargoOperationDto>>(_cargoOperationService.TGetAll());
            return Ok(values);
        }

        [HttpGet("{id}")]
        public IActionResult GetCargoOperation(int id)
        {
            var value = _mapper.Map<GetByIdCargoOperationDto>(_cargoOperationService.TGetById(id));
            return Ok(value);
        }

        [HttpPost]
        public IActionResult CreateCargoOperation(CreateCargoOperationDto createCargoOperationDto)
        {
            _cargoOperationService.TInsert(_mapper.Map<CargoOperation>(createCargoOperationDto));
            return Ok("Başarılı");
        }

        [HttpPut]
        public IActionResult UpdateCargoOperation(UpdateCargoOperationDto updateCargoOperationDto)
        {
            _cargoOperationService.TUpdate(_mapper.Map<CargoOperation>(updateCargoOperationDto));
            return Ok("Başarılı");
        }

        [HttpDelete]
        public IActionResult DeleteCargoOperation(int id)
        {
            _cargoOperationService.TDelete(id);
            return Ok("Başarılı");
        }

        [HttpGet("ByBarcode/{barcode}")]
        public IActionResult GetByBarcode(string barcode)
        {
            var values = _mapper.Map<List<ResultCargoOperationDto>>(_cargoOperationService.TGetByBarcode(barcode));
            return Ok(values);
        }
    }
}
