using BookHubMvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookHubMVC.Controllers
{
    public class BooksController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public BooksController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient();
            _apiBaseUrl = configuration["ApiBaseUrl"] ?? "";
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/books");
            if (!response.IsSuccessStatusCode)
            {
                return View("Error"); // You can create a simple error view later
            }

            var books = await response.Content.ReadFromJsonAsync<List<BookReadDto>>();
            return View(books);
        }

        //Details action to fetch a single book by ID
        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/books/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return NotFound(); // or a custom error view
            }

            var book = await response.Content.ReadFromJsonAsync<BookReadDto>();
            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookUpdateDto newBook)
        {
            if (!ModelState.IsValid)
            {
                return View(newBook);
            }

            var response = await _httpClient.PostAsJsonAsync($"{_apiBaseUrl}/books", newBook);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Failed to create the book.");
                return View(newBook);
            }

            return RedirectToAction(nameof(Index));
        }


        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/books/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var book = await response.Content.ReadFromJsonAsync<BookUpdateDto>();
            ViewBag.BookId = id;
            return View(book);
        }

        // POST: Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BookUpdateDto updatedBook)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.BookId = id;
                return View(updatedBook);
            }

            var response = await _httpClient.PutAsJsonAsync($"{_apiBaseUrl}/books/{id}", updatedBook);
            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Failed to update the book.");
                ViewBag.BookId = id;
                return View(updatedBook);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/books/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var book = await response.Content.ReadFromJsonAsync<BookReadDto>();
            return View(book); // Confirmation View
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_apiBaseUrl}/books/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Delete), new { id });
            }

            return RedirectToAction(nameof(Index));
        }



    }
}
