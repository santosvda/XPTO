using System.Linq;
using AutoMapper;
using XPTO.API.Dtos;
using XPTO.Domain;

namespace XPTO.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {

            CreateMap<Product, ProductDto>().ReverseMap();
        }
    }
}