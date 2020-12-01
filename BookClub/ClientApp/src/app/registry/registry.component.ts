import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

import { ToastrService, ToastrModel, ToastrType } from '../toastr/toastr.service'

@Component({
  selector: 'app-registry',
  templateUrl: './registry.component.html',
  styleUrls: ['./registry.component.css']
})
export class RegistryComponent implements OnInit {

  private _http: HttpClient;
  private _baseUrl: string;
  private _toastr: ToastrService;

  public username: string = "";
  public password: string = "";

  /**
   * Конструктор
   * @param http - клиент для ображения на сервер ASP.NET Core
   * @param baseUrl - базовая ссылка
   */
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string,
    toastr: ToastrService,
    private _router: Router) {
    this._http = http;
    this._baseUrl = baseUrl;
    this._toastr = toastr;
  }

  ngOnInit(): void {
  }

  /**
   * Метод регистрации на сервере
   * */
  public registry(): void {
    //модель аутентификации
    const model: RegistryModel = {
      UserName: this.username,
      Password: this.password,
    }
    //запрос на сервер для аутентификации
    this._http.post<RegistryModel>(this._baseUrl + 'api/auth/registry', model).subscribe(result => {
      this._toastr.showToast(new ToastrModel(ToastrType.success, "Регистрация прошла успешно"));
      this._router.navigate(['/login']);
    }, error => {
        console.error(error);
        this._toastr.showToast(new ToastrModel(ToastrType.error, error.error.title));
    });
  }
}

interface RegistryModel {
  UserName: string;
  Password: string;
}
