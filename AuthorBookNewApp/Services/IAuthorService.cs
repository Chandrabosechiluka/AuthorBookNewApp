using AuthorBookNewApp.DTOs.Author;
using AuthorBookNewApp.DTOs.Book;

namespace AuthorBookNewApp.Services
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorResponseDto>> GetAllAsync();
        Task<AuthorResponseDto> GetByIdAsync(int id);
        Task<AuthorResponseDto> CreateAsync(AuthorCreateDto dto);
        Task UpdateAsync(int id, AuthorUpdateDto dto);
        Task DeleteAsync(int id);
        Task<IEnumerable<BookResponseDto>> GetBooksByAuthorIdAsync(int authorId);
    }
}
