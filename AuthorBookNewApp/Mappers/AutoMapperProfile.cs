using AuthorBookNewApp.DTOs.Author;
using AuthorBookNewApp.DTOs.Book;
using AuthorBookNewApp.Models;
using AutoMapper;

namespace AuthorBookNewApp.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Author, AuthorResponseDto>()
                .ForMember(dest => dest.TotalBooks, opt => opt.MapFrom(src => src.Books.Count));
            CreateMap<AuthorCreateDto, Author>();
            CreateMap<AuthorUpdateDto, Author>().ForAllMembers(opts =>
                        opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Book, BookResponseDto>()
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name));
            CreateMap<BookCreateDto, Book>();
            CreateMap<BookUpdateDto, Book>()
                //.ForMember(dest => dest.AuthorId,
                //                opt => opt.PreCondition(src => src.AuthorId.HasValue))
                .ForAllMembers(opts =>
                        opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
