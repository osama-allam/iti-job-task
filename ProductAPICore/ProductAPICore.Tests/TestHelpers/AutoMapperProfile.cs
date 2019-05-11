using ProductAPICore.API.ViewModels;
using ProductAPICore.Model.Core.Domains;

namespace ProductAPICore.Tests.TestHelpers
{
    public class AutoMapperProfile : AutoMapper.Profile
    {

        public static void Configure()
        {
            AutoMapper.Mapper.Initialize(a =>
            {
                a.AddProfile<AutoMapperProfile>();
            });
        }

        public AutoMapperProfile()
        {
            CreateMap<Product, GetProductViewModel>()
                .ForMember(dest => dest.CompanyName,
                    opt => opt.MapFrom(src => src.Company.Name))
                .ForMember(dest => dest.CompanyId,
                    opt => opt.MapFrom(src => src.Company.Id));


            CreateMap<Product, UpdateProductViewModel>();

            CreateMap<UpdateProductViewModel, Product>();


            CreateMap<Company, GetCompanyViewModel>();
        }
    }
}
