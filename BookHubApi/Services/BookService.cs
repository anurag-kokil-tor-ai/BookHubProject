using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookHubApi.Data;
using BookHubApi.DTOs;
using BookHubApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BookHubApi.Services
{
    public class BookService : IBookService
    {
        private readonly BookDbContext _context;
        private readonly IMapper _mapper;

        public BookService(BookDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<BookReadDto>> GetAllAsync()
        {
            var books = await _context.Books.ToListAsync();
            return _mapper.Map<List<BookReadDto>>(books);

            //instead we can also use:
            //return await _context.Books
            //.ProjectTo<BookReadDto>(_mapper.ConfigurationProvider)
            //.ToListAsync();
        }

        public async Task<BookReadDto?> GetByIdAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return null;

            return _mapper.Map<BookReadDto>(book);
        }

        public async Task<BookReadDto> CreateAsync(BookCreateDto dto)
        {
            var book = _mapper.Map<Book>(dto);
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return _mapper.Map<BookReadDto>(book);
        }

        public async Task<bool> UpdateAsync(int id, BookUpdateDto dto)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return false;

            _mapper.Map(dto, book);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return false;

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<BookReadDto>> GetBooksByMinPriceAsync(decimal minPrice)
        {
            var books = await _context.Books
                .Where(b => b.Price > minPrice)
                .ToListAsync();

            return _mapper.Map<List<BookReadDto>>(books);
        }

        public async Task<List<BookReadDto>> GetBooksByMaxpriceAsync(decimal maxPrice)
        {
            var books = await _context.Books
                .Where(b => b.Price < maxPrice)
                .ToListAsync();
            return _mapper.Map<List<BookReadDto>>(books);
        }

        public async Task<List<BookReadDto>> GetBooksByAuthorAsync(string author)
        {
            var books = await _context.Books
                .Where(b => b.Author.ToLower().Contains(author.ToLower()))
                .ToListAsync();
            return _mapper.Map<List<BookReadDto>>(books);
        }
    }
}
