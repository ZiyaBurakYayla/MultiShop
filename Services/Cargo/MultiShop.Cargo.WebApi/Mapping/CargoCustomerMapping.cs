using AutoMapper;
using MultiShop.Cargo.DtoLayer.Dtos.CargoCustomerDtos;
using MultiShop.Cargo.EntityLayer.Concrete;

namespace MultiShop.Cargo.WebApi.Mapping
{
    public class CargoCustomerMapping : Profile
    {
        public CargoCustomerMapping()
        {
            CreateMap<CargoCustomer, ResultCargoCustomerDto>().ReverseMap();
            CreateMap<CargoCustomer, CreateCargoCustomerDto>().ReverseMap();
            CreateMap<CargoCustomer, UpdateCargoCustomerDto>().ReverseMap();
            CreateMap<CargoCustomer, GetByIdCargoCustomerDto>().ReverseMap();
        }
    }
}
