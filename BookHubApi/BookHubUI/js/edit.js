const API_BASE = "https://localhost:7234/api/books";
const params = new URLSearchParams(window.location.search);
const bookId = params.get("id");

fetch(`${API_BASE}/${bookId}`)
    .then(res => res.json())
    .then(book => {
        document.getElementById("title").value = book.title;
        document.getElementById("author").value = book.author;
        document.getElementById("price").value = book.price;
    });

document.getElementById("edit-form").addEventListener("submit", function (e) {
    e.preventDefault();

    const updatedBook = {
        title: document.getElementById("title").value,
        author: document.getElementById("author").value,
        price: parseFloat(document.getElementById("price").value)
    };

    fetch(`${API_BASE}/${bookId}`, {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(updatedBook)
    })
        .then(res => {
            if (res.ok) window.location.href = "index.html";
            else alert("Failed to update book.");
        });
});
