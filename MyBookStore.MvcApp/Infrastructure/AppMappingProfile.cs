using AutoMapper;
using MyBookStore.MvcApp.Models;
using MyBookStore.MvcApp.Models.ViewModels.Admin;

namespace MyBookStore.MvcApp.Infrastructure;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<BookDTO, Book>().ReverseMap();
    }
}