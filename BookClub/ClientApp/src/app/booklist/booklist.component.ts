import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { BookList, BookModel } from '../shared/book-list';

import { ToastrService, ToastrModel, ToastrType } from '../toastr/toastr.service';

@Component({
  selector: 'app-booklist',
  templateUrl: './booklist.component.html',
  styleUrls: ['./booklist.component.css']
})

export class BookListComponent extends BookList implements OnInit  {

  constructor(http: HttpClient,
    @Inject('BASE_URL') baseUrl: string,
    toastr: ToastrService) {
    super(http, baseUrl, toastr, 'api/book/booklist');
  }

  ngOnInit(): void {
  }

  addBook(bookId: number): void {
    const model: BookModel = {
      lnkId: null,
      bookId: bookId,
      bookName: null
    }
    this._http.post(this._baseUrl + 'api/book/addbook', model).subscribe(result => {
      this._toastr.showToast(new ToastrModel(ToastrType.success, "Книга добавлена в список прочитанных"));
      this.getBookList();
    }, error => {
      this._toastr.showToast(new ToastrModel(ToastrType.error, error.error));
      console.error(error);
    });
  }
}

