import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from './auth.service';
import { ToastrService, ToastrModel, ToastrType } from '../toastr/toastr.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private _authService: AuthService,
    private _router: Router,
    private _toastr: ToastrService) {

  }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    //обращение в этот метод происходит до обращения в клас компонента
    if (this._authService.isLogIn !== true) {
      //если аутентификация не пройден, тогда выкидываем на страницу входа
      this._toastr.showToast(new ToastrModel(ToastrType.error, "Доступ запрещен"));
      this._router.navigate(['/login']);
    }
    return true;
  }
  
}
