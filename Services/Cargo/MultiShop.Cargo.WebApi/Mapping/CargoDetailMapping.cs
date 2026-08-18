using AutoMapper;
using MultiShop.Cargo.DtoLayer.Dtos.CargoDetailDtos;
using MultiShop.Cargo.EntityLayer.Concrete;

namespace MultiShop.Cargo.WebApi.Mapping
{
    public class CargoDetailMapping : Profile
    {
        public CargoDetailMapping()
        {
            CreateMap<CargoDetail, ResultCargoDetailDto>()
                .ForMember(x => x.CargoCompanyName, opt => opt.MapFrom(y => y.CargoCompany.CargoCompanyName));
            CreateMap<ResultCargoDetailDto, CargoDetail>();
            CreateMap<CargoDetail, CreateCargoDetailDto>().ReverseMap();
            CreateMap<CargoDetail, UpdateCargoDetailDto>().ReverseMap();
            CreateMap<CargoDetail, GetByIdCargoDetailDto>().ReverseMap();
        }
    }
}
