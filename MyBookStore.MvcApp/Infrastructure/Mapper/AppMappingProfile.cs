using AutoMapper;
using MyBookStore.MvcApp.Models;
using MyBookStore.MvcApp.Models.DTO;

namespace MyBookStore.MvcApp.Infrastructure;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<BookDTO, Book>().ReverseMap();
        CreateMap<User, User>().ReverseMap()
            .ForAllMembers(opts
                => opts.Condition((src, dest, srcMember) => srcMember != null));
        ;
    }
}