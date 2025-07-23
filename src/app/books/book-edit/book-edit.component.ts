import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { BookService } from '../../services/book.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-book-edit',
  templateUrl: './book-edit.component.html',
  styleUrls: ['./book-edit.component.css']
})
export class BookEditComponent implements OnInit {
  bookForm!: FormGroup;
  bookId!: number;

constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private bookService: BookService,
    private router: Router
  ) {}
  
  ngOnInit(): void {
    this.bookId = Number(this.route.snapshot.paramMap.get('id'));

    this.bookForm = this.fb.group({
      title: ['', Validators.required],
      author: ['', Validators.required],
      price: [0, [Validators.required, Validators.min(1)]]
    });

    this.bookService.getBookById(this.bookId).subscribe({
      next: (book) => this.bookForm.patchValue(book),
      error: (err) => console.error('Error fetching book:', err)
    });
  }

  onSubmit(): void {
    if (this.bookForm.valid) {
      this.bookService.editBook(this.bookId, this.bookForm.value).subscribe({
        next: () => {
          console.log('Book details updated successfully');
          this.router.navigate(['/books']);
        },
        error: (err) => console.error('Error updating book', err)
      });
    }
  }

}
