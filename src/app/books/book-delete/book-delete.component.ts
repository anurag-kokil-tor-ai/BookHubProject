import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { BookService } from '../../services/book.service';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-book-delete',
  templateUrl: './book-delete.component.html',
  styleUrls: ['./book-delete.component.css']
})
export class BookDeleteComponent implements OnInit {
  bookId!: number;

  constructor(
    private route: ActivatedRoute,
    private bookService: BookService,
    private router: Router
  ) { }

  ngOnInit(): void {
    const idParam = this.route.snapshot.paramMap.get('id');
    this.bookId = idParam ? +idParam : NaN;
    if (isNaN(this.bookId)) {
      alert('Invalid book ID');
      this.router.navigate(['/books']);
    }
  }

  deleteBook(): void {
    this.bookService.deleteBook(this.bookId).subscribe({
      next: () => {
        console.log('Book deleted successfully');
        this.router.navigate(['/books']);
      },
      error: (err) => {
        console.error('Error deleting book', err);
      }
    });
  }

  cancel(): void {
    this.router.navigate(['/books']);
  }

}
