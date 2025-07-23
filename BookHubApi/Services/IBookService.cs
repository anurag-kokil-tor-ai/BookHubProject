using BookHubApi.DTOs;

namespace BookHubApi.Services
{
    public interface IBookService
    {
        Task<List<BookReadDto>> GetAllAsync();
        Task<BookReadDto?> GetByIdAsync(int id);
        Task<BookReadDto> CreateAsync(BookCreateDto dto);
        Task<bool> UpdateAsync(int id, BookUpdateDto dto);
        Task<bool> DeleteAsync(int id);
        Task<List<BookReadDto>> GetBooksByMinPriceAsync(decimal minPrice);
        Task<List<BookReadDto>> GetBooksByMaxpriceAsync(decimal maxPrice);
        Task<List<BookReadDto>> GetBooksByAuthorAsync(string author);
    }
}
