const API_BASE = "https://localhost:7234/api/books";

document.getElementById("create-form").addEventListener("submit", function (e) {
    e.preventDefault();

    const book = {
        title: document.getElementById("title").value,
        author: document.getElementById("author").value,
        price: parseFloat(document.getElementById("price").value)
    };

    fetch(API_BASE, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(book)
    })
        .then(res => {
            if (res.ok) window.location.href = "index.html";
            else alert("Failed to add book.");
        });
});
