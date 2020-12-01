import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { BookList, BookModel } from '../shared/book-list';

import { ToastrService, ToastrModel, ToastrType} from '../toastr/toastr.service';

@Component({
  selector: 'app-mybooklist',
  templateUrl: './mybooklist.component.html',
  styleUrls: ['./mybooklist.component.css']
})
export class MyBookListComponent extends BookList implements OnInit {

  constructor(http: HttpClient,
    @Inject('BASE_URL') baseUrl: string,
    toastr: ToastrService) {
    super(http, baseUrl, toastr, 'api/book/mybooklist');
  }

  ngOnInit(): void {
  }

  removeBook(lnkBookId: number): void {
    const model: BookModel = {
      lnkId: lnkBookId,
      bookId: null,
      bookName: null
    }
    this._http.post(this._baseUrl + 'api/book/removebook', model)
      .subscribe(result => {
        this._toastr.showToast(new ToastrModel(ToastrType.success, "Книга удалена из списка прочитанных"));
        this.getBookList();
      }, error => {
        this._toastr.showToast(new ToastrModel(ToastrType.error, error.error));
        console.error(error);
      });
  }
}
