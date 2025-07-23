import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BookListComponent } from './components/book-list/book-list.component';
import { BookDetailsComponent } from './books/book-details/book-details.component';
import { BookCreateComponent } from './books/book-create/book-create.component';
import { BookEditComponent } from './books/book-edit/book-edit.component';
import { BookDeleteComponent } from './books/book-delete/book-delete.component';

const routes: Routes = [
  { path: '', redirectTo: '/books', pathMatch: 'full'},
  { path: 'books', component: BookListComponent },
  { path: 'books/create', component: BookCreateComponent },
  { path: 'books/:id', component: BookDetailsComponent },
  { path: 'books/edit/:id', component: BookEditComponent },
  { path: 'books/delete/:id', component: BookDeleteComponent },
  // Add more routes as needed
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
