using AutoMapper;
using BookStore.DAL.Models;
using BookStore.PL.ViewModels;

namespace BookStore.PL.MappingProfiles
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book,BookViewModel>().ReverseMap();
        }
    }
}
