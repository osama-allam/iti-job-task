using ProductAPICore.API.ViewModels;
using ProductAPICore.Model.Core.Domains;

namespace ProductAPICore.Tests.TestHelpers
{
    public class AutoMapperHelper
    {
        public static void Configure()
        {

            AutoMapper.Mapper.Initialize(config =>
            {
                config.CreateMap<Product, GetProductViewModel>()
                    .ForMember(dest => dest.CompanyName,
                        opt => opt.MapFrom(src => src.Company.Name))
                    .ForMember(dest => dest.CompanyId,
                        opt => opt.MapFrom(src => src.Company.Id));


                config.CreateMap<Product, UpdateProductViewModel>();

                config.CreateMap<UpdateProductViewModel, Product>();


                config.CreateMap<Company, GetCompanyViewModel>();
            });
        }
    }
}
