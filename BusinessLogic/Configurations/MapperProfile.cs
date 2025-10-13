using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.DTOs.Account;
using DataAccess.Data.Entities;

namespace BusinessLogic.Configurations
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CreateProductDto, Product>();
            CreateMap<EditProductDto, Product>();
            CreateMap<ProductDto, Product>().ReverseMap();
            CreateMap<CategoryDto, Category>().ReverseMap();

            CreateMap<RegisterModel, User>()
                .ForMember(x => x.UserName, opt => opt.MapFrom(model => model.Email))
                .ForMember(x => x.PasswordHash, opt => opt.Ignore());
        }
    }
}
