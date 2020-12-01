import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root'
})

/**
 * Сервис получения проверки аутентификации
 * */
export class AuthService {

  constructor(private _cookie: CookieService) {
  }

  /**
   *  Проверка аутентификации
   * */
  get isLogIn(): boolean {
    return this._cookie.check(".AspNetCore.Application.System");
  }
}
