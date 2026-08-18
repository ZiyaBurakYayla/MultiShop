using AutoMapper;
using MultiShop.Cargo.DtoLayer.Dtos.CargoCompanyDtos;
using MultiShop.Cargo.EntityLayer.Concrete;

namespace MultiShop.Cargo.WebApi.Mapping
{
    public class CargoCompanyMapping : Profile
    {
        public CargoCompanyMapping()
        {
            CreateMap<CargoCompany, ResultCargoCompanyDto>().ReverseMap();
            CreateMap<CargoCompany, CreateCargoCompanyDto>().ReverseMap();
            CreateMap<CargoCompany, UpdateCargoCompanyDto>().ReverseMap();
            CreateMap<CargoCompany, GetByIdCargoCompanyDto>().ReverseMap();
        }
    }
}
