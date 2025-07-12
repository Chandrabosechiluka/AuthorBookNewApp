using AuthorBookNewApp.DTOs.Book;

namespace AuthorBookNewApp.Services
{
    public interface IBookService
    {
        Task<IEnumerable<BookResponseDto>> GetAllAsync();
        Task<BookResponseDto> GetByIdAsync(int id);
        Task<BookResponseDto> CreateAsync(BookCreateDto dto);
        Task UpdateAsync(int id, BookUpdateDto dto);
        Task DeleteAsync(int id);
        Task<IEnumerable<BookResponseDto>> SearchByTitleAsync(string title);
    }
}
