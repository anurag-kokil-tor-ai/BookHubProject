using BookHubApi.DTOs;
using BookHubApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookHubApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<List<BookReadDto>>> GetAll()
        {
            return Ok(await _bookService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookReadDto>> GetById(int id)
        {
            var book = await _bookService.GetByIdAsync(id);
            if (book == null) return NotFound();
            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult<BookReadDto>> Create(BookCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var created = await _bookService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, BookUpdateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var success = await _bookService.UpdateAsync(id, dto);
            if (!success) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _bookService.DeleteAsync(id);
            if (!success) return NotFound();

            return NoContent();
        }

        [HttpGet("price-above/{minPrice}")]
        public async Task<ActionResult<List<BookReadDto>>> GetByMinPrice(decimal minPrice)
        {
            var books = await _bookService.GetBooksByMinPriceAsync(minPrice);
            return Ok(books);
        }

        [HttpGet("price-below/{maxPrice}")]
        public async Task<ActionResult<List<BookReadDto>>> GetByMaxPrice(decimal maxPrice)
        {
            var books = await _bookService.GetBooksByMaxpriceAsync(maxPrice);
            return Ok(books);
        }

        [HttpGet("author/{author}")]
        public async Task<ActionResult<List<BookReadDto>>> GetByAuthor(string author)
        {
            var books = await _bookService.GetBooksByAuthorAsync(author);
            if (books == null || books.Count == 0) return NotFound();
            return Ok(books);
        }
    }
}
