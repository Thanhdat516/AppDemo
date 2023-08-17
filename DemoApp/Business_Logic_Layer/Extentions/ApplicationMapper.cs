using AutoMapper;
using Business_Logic_Layer.Models;
using Data_Access_Layer.Entities;

namespace Business_Logic_Layer.Extentions
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Company, CompanyModel>().ReverseMap();
        }
    }
}
