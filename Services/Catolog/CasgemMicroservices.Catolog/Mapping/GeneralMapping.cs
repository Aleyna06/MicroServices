using AutoMapper;
using CasgemMicroservices.Catolog.Dtos.CategoryDtos;
using CasgemMicroservices.Catolog.Dtos.ProductDtos;
using CasgemMicroservices.Catolog.Models;

namespace CasgemMicroservices.Catolog.Mapping
{
    public class GeneralMapping:Profile
    {
        public GeneralMapping()
        {
            CreateMap<Category, ResultCategoryDto>().ReverseMap();
            CreateMap<Category, CreateCategoryDto>().ReverseMap();
            CreateMap<Category, UpdateCategoryDto>().ReverseMap();


            CreateMap<Product, ResultProductDto>().ReverseMap();
            CreateMap<Product, CreateProductDto>().ReverseMap();
            CreateMap<Product, UpdateProductDto>().ReverseMap();


        }
    }
}
