using AuthorBookNewApp.Data;
using AuthorBookNewApp.DTOs.Author;
using AuthorBookNewApp.DTOs.Book;
using AuthorBookNewApp.Exceptions.Author;
using AuthorBookNewApp.Models;
using AuthorBookNewApp.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AuthorBookNewApp.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IGenericRepository<Author> _authorRepo;
        private readonly IGenericRepository<Book> _bookRepo;
        private readonly IMapper _mapper;

        public AuthorService(IGenericRepository<Author> authorRepo, IMapper mapper, IGenericRepository<Book> bookRepo)
        {
            _authorRepo = authorRepo;
            _mapper = mapper;
            _bookRepo = bookRepo;
        }

        public async Task<IEnumerable<AuthorResponseDto>> GetAllAsync()
        {
            
            var authors= await _authorRepo.GetAll().Include(a => a.Books).ToListAsync();
            
            return _mapper.Map<IEnumerable<AuthorResponseDto>>(authors);
        }

        public async Task<AuthorResponseDto> GetByIdAsync(int id)
        {
            var author= await _authorRepo.GetByIdAsync(id);
            
            if (author == null)
                throw new AuthorNotFoundException($"Author with ID {id} not found.");
            
            return _mapper.Map<AuthorResponseDto>(author);
        }

        public async Task<AuthorResponseDto> CreateAsync(AuthorCreateDto dto)
        {
            //AutoMapping
            var author = _mapper.Map<Author>(dto);

            //Manually mapping dto data into author 
            //var author = new Author
            //{
            //    Name = dto.Name,
            //    Email = dto.Email,
            //    Bio = dto.Bio
            //};
            await _authorRepo.AddAsync(author);

            //Manually mapping author data into authorResponseDto
            //var authorResponseDto = new AuthorResponseDto()
            //{
            //    Name=author.Name,
            //    Email=author.Email,
            //    Bio=author.Bio,
            //    TotalBooks=author.Books.Count
            //};
            //return authorResponseDto;
            return _mapper.Map<AuthorResponseDto>(author);
        }

        public async Task UpdateAsync(int id, AuthorUpdateDto dto)
        {
            var author = await _authorRepo.GetByIdAsync(id);
            if (author == null)
                throw new AuthorNotFoundException($"Author with ID {id} not found.");

            
            _mapper.Map(dto, author);
            
            await _authorRepo.UpdateAsync(author);
            //return _mapper.Map<AuthorResponseDto>(author);
        }

        public async Task DeleteAsync(int id)
        {
            var author = await _authorRepo.GetByIdAsync(id);
            if (author == null)
                throw new AuthorNotFoundException($"Author with ID {id} not found.");

            await _authorRepo.DeleteAsync(author);
        }

        public async Task<IEnumerable<BookResponseDto>> GetBooksByAuthorIdAsync(int authorId)
        {
            var books = await _bookRepo.GetAll().Where(b => b.AuthorId == authorId)
                .Include(b => b.Author)
                .ToListAsync();

            return _mapper.Map<IEnumerable<BookResponseDto>>(books);
        }
    }
}
