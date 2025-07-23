const API_BASE = "https://localhost:7234/api/books";

document.addEventListener("DOMContentLoaded", () => {
    const container = document.querySelector("#books-container");
    if (!container) return;

    fetch(API_BASE)
        .then(res => res.json())
        .then(books => {
            books.forEach(book => {
                const card = document.createElement("div");
                card.className = "col-md-4 mb-4";
                card.innerHTML = `
          <div class="card h-100 shadow-sm">
            <div class="card-body">
              <h5 class="card-title">${book.title}</h5>
              <h6 class="card-subtitle mb-2 text-muted">By ${book.author}</h6>
              <p class="card-text">Price: ₹${book.price}</p>
              <a href="edit.html?id=${book.id}" class="btn btn-sm btn-primary me-2">Edit</a>
              <button class="btn btn-sm btn-danger" onclick="deleteBook(${book.id})">Delete</button>
            </div>
          </div>`;
                container.appendChild(card);
            });
        });
});

function deleteBook(id) {
    if (!confirm("Are you sure you want to delete this book?")) return;
    fetch(`${API_BASE}/${id}`, { method: "DELETE" })
        .then(() => location.reload());
}
