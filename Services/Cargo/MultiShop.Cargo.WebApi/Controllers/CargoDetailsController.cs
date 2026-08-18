using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.DtoLayer.Dtos.CargoDetailDtos;
using MultiShop.Cargo.EntityLayer.Concrete;
using MultiShop.Cargo.WebApi.LoginServices;

namespace MultiShop.Cargo.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CargoDetailsController : ControllerBase
    {
        private readonly ICargoDetailService _cargoDetailService;
        private readonly ICargoOperationService _cargoOperationService;
        private readonly ILoginService _loginService;
        private readonly IMapper _mapper;

        public CargoDetailsController(ICargoDetailService cargoDetailService, ICargoOperationService cargoOperationService, ILoginService loginService, IMapper mapper)
        {
            _cargoDetailService = cargoDetailService;
            _cargoOperationService = cargoOperationService;
            _loginService = loginService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult CargoDetailList()
        {
            var values = _mapper.Map<List<ResultCargoDetailDto>>(_cargoDetailService.TGetAllWithCompany());
            return Ok(values);
        }

        [HttpGet("{id}")]
        public IActionResult GetCargoDetail(int id)
        {
            var value = _mapper.Map<GetByIdCargoDetailDto>(_cargoDetailService.TGetById(id));
            return Ok(value);
        }

        [HttpPost]
        public IActionResult CreateCargoDetail(CreateCargoDetailDto createCargoDetailDto)
        {
            _cargoDetailService.TInsert(_mapper.Map<CargoDetail>(createCargoDetailDto));
            _cargoOperationService.TInsert(new CargoOperation
            {
                Barcode = createCargoDetailDto.Barcode,
                Description = "Kargo kaydı oluşturuldu",
                OperationDate = DateTime.Now
            });
            return Ok("Başarılı");
        }

        [HttpPut]
        public IActionResult UpdateCargoDetail(UpdateCargoDetailDto updateCargoDetailDto)
        {
            _cargoDetailService.TUpdate(_mapper.Map<CargoDetail>(updateCargoDetailDto));
            return Ok("Başarılı");
        }

        [HttpDelete]
        public IActionResult DeleteCargoDetail(int id)
        {
            _cargoDetailService.TDelete(id);
            return Ok("Başarılı");
        }

        [HttpGet("MyCargos")]
        public IActionResult MyCargos()
        {
            var userId = _loginService.GetUserId;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("Kullanıcı kimliği bulunamadı.");
            }
            var values = _mapper.Map<List<ResultCargoDetailDto>>(_cargoDetailService.TGetByUserId(userId));
            return Ok(values);
        }

        [HttpGet("ByBarcode/{barcode}")]
        public IActionResult GetByBarcode(string barcode)
        {
            var value = _cargoDetailService.TGetByBarcode(barcode);
            if (value == null)
            {
                return NotFound("Kargo bulunamadı.");
            }
            return Ok(_mapper.Map<ResultCargoDetailDto>(value));
        }

        [HttpGet("ByCompanyId/{companyId}")]
        public IActionResult GetByCompanyId(int companyId)
        {
            var values = _mapper.Map<List<ResultCargoDetailDto>>(_cargoDetailService.TGetByCompanyId(companyId));
            return Ok(values);
        }

        [HttpPut("UpdateStatus")]
        public IActionResult UpdateStatus(UpdateCargoStatusDto updateCargoStatusDto)
        {
            var value = _cargoDetailService.TGetByBarcode(updateCargoStatusDto.Barcode);
            if (value == null)
            {
                return NotFound("Kargo bulunamadı.");
            }
            value.Status = updateCargoStatusDto.Status;
            _cargoDetailService.TUpdate(value);
            _cargoOperationService.TInsert(new CargoOperation
            {
                Barcode = value.Barcode,
                Description = updateCargoStatusDto.Status,
                OperationDate = DateTime.Now
            });
            return Ok("Başarılı");
        }
    }
}
