using AutoMapper;
using Domain.Entities;

namespace Application.Products.Queries.GetProducts;

public class Mapping : Profile
{
    public Mapping()
    {
        CreateMap<Product, ProductDto>()
            .ForMember(x => x.ProductType, opt => opt.MapFrom(dest => dest.ProductType!.Name));
    }
}
