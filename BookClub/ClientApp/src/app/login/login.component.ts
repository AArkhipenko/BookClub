import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

import { ToastrService, ToastrModel, ToastrType } from '../toastr/toastr.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  public username: string = "";
  public password: string = "";

  /**
   * Конструктор
   * @param http - клиент для ображения на сервер ASP.NET Core
   * @param baseUrl - базовая ссылка
   */
  constructor(private _http: HttpClient,
    @Inject('BASE_URL') private _baseUrl: string,
    private _toastr: ToastrService,
    private _router: Router
  ) {
  }

  ngOnInit(): void {
  }

  /**
   * Метод аутентификации на сервере
   * */
  public logIn() {
    //модель аутентификации
    const model: LoginModel = {
      UserName: this.username,
      Password: this.password,
    }
    //запрос на сервер для аутентификации
    this._http.post<LoginModel>(this._baseUrl + 'api/auth/login', model).subscribe(result => {
      this._toastr.showToast(new ToastrModel(ToastrType.success, "Вход выполнен"));
      this._router.navigate(['/booklist']);
    }, error => {
        this._toastr.showToast(new ToastrModel(ToastrType.error, error.error));
        console.error(error);
    });
  }
}

interface LoginModel {
  UserName: string;
  Password: string;
}
