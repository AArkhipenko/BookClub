import { Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { ToastrService, ToastrModel, ToastrType } from '../toastr/toastr.service';

export class BookList {

  public bookList: BookModel[];
  public bookName: string;

  constructor(protected _http: HttpClient,
    protected _baseUrl: string,
    protected _toastr: ToastrService,
    private _getBookUrl: string) {

    this.getBookList();
  }

  getBookList(): void {
    const model: BookModel = {
      lnkId: null,
      bookId: null,
      bookName: this.bookName
    }
    this._http.post<BookModel[]>(this._baseUrl + this._getBookUrl, model)
      .subscribe(result => {
        this.bookList = result;
      }, error => {
        this._toastr.showToast(new ToastrModel(ToastrType.error, ""));
        console.error(error);
      });
  }
}

export interface BookModel {
  lnkId: number | null;
  bookId: number | null;
  bookName: string;
}
