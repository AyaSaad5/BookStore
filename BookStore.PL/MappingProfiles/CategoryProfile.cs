using AutoMapper;
using BookStore.DAL;

namespace BookStore.PL.MappingProfiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryProfile,Category >().ReverseMap();
        }
    }
}
