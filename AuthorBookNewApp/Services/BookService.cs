using AuthorBookNewApp.Data;
using AuthorBookNewApp.DTOs.Book;
using AuthorBookNewApp.Exceptions.Author;
using AuthorBookNewApp.Exceptions.Book;
using AuthorBookNewApp.Models;
using AuthorBookNewApp.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AuthorBookNewApp.Services
{
    public class BookService : IBookService
    {
        private readonly IGenericRepository<Book> _bookRepo;
        private readonly IGenericRepository<Author> _authorRepo;
        private readonly IMapper _mapper;


        public BookService(IGenericRepository<Book> bookRepo, IMapper mapper, IGenericRepository<Author> authorRepo)
        {
            _bookRepo = bookRepo;
            _mapper = mapper;
            _authorRepo = authorRepo;
        }

        public async Task<IEnumerable<BookResponseDto>> GetAllAsync()
        {
            var books = await _bookRepo.GetAll().Include(b => b.Author).ToListAsync();
            return _mapper.Map<IEnumerable<BookResponseDto>>(books);
        }

        public async Task<BookResponseDto> GetByIdAsync(int id)
        {
            var book = await _bookRepo.GetAll()
                .Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.Id == id);
            if (book == null)
                throw new BookNotFoundException($"Book with ID {id} not found.");

            return _mapper.Map<BookResponseDto>(book);
        }

        public async Task<BookResponseDto> CreateAsync(BookCreateDto dto)
        {
            var author = await _authorRepo.GetByIdAsync(dto.AuthorId);
            if (author==null)
                throw new AuthorNotFoundException($"Author with ID {dto.AuthorId} does not exist.");

            var book = _mapper.Map<Book>(dto);
            await _bookRepo.AddAsync(book);
            return _mapper.Map<BookResponseDto>(book);
        }

        public async Task UpdateAsync(int id, BookUpdateDto dto)
        {
            var book = await _bookRepo.GetByIdAsync(id);
            if (book == null)
                throw new BookNotFoundException($"Book with ID {id} not found.");

            // Validate if new AuthorId is valid
            var authorId = dto.AuthorId;
            var authorExists = await _authorRepo.GetByIdAsync(authorId);
            if (authorExists==null)
                throw new Exception($"Author with ID {authorId} does not exist.");

            _mapper.Map(dto, book);
            //book.AuthorId = authorId;=>this is for mapping if i keep author id as nullable
            await _bookRepo.UpdateAsync(book);
        }

        public async Task DeleteAsync(int id)
        {
            var book = await _bookRepo.GetByIdAsync(id);
            if (book == null)
                throw new Exception($"Book with ID {id} not found.");

            await _bookRepo.DeleteAsync(book);
        }

        public async Task<IEnumerable<BookResponseDto>> SearchByTitleAsync(string title)
        {
            var books = await _bookRepo.GetAll()
                .Include(b => b.Author)
                .Where(b => b.Title.Contains(title))
                .ToListAsync();

            return _mapper.Map<IEnumerable<BookResponseDto>>(books);
        }
    }
}
