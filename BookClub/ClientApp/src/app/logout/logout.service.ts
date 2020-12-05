import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { ToastrService, ToastrModel, ToastrType } from '../toastr/toastr.service'

@Injectable({
  providedIn: 'root'
})
export class LogoutService {

  constructor(private _http: HttpClient,
    @Inject('BASE_URL') private _baseUrl: string,
    private _toastr: ToastrService) {
  }

  ngOnInit(): void {
  }

  public logOut() {
    //запрос на сервер для выхода
    this._http.post(this._baseUrl + 'api/auth/logout', {}).subscribe(result => {
      this._toastr.showToast(new ToastrModel(ToastrType.success, "Выход выполнен"));
    }, error => {
      this._toastr.showToast(new ToastrModel(ToastrType.error, error.error.title));
      console.error(error);
    });
  }
}
