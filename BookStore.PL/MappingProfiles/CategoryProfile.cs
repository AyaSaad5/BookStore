using AutoMapper;
using BookStore.DAL;
using BookStore.PL.ViewModels;

namespace BookStore.PL.MappingProfiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryViewModel,Category >().ReverseMap();
        }
    }
}
