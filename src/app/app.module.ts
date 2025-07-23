import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BookListComponent } from './components/book-list/book-list.component';
import { BookDetailsComponent } from './books/book-details/book-details.component';
import { BookCreateComponent } from './books/book-create/book-create.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BookEditComponent } from './books/book-edit/book-edit.component';
import { BookDeleteComponent } from './books/book-delete/book-delete.component';

@NgModule({
  declarations: [
    AppComponent,
    BookListComponent,
    BookDetailsComponent,
    BookCreateComponent,
    BookEditComponent,
    BookDeleteComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
