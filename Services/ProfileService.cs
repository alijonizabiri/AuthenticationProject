using AutoMapper;
using Domain;
using Domain.Models;

namespace Services;

public class ProfileService : Profile
{
    public ProfileService()
    {
        CreateMap<ProductDto, Product>();
        CreateMap<CustomerDto, Customer>();
        CreateMap<CategoryDto, Category>();
    }
}