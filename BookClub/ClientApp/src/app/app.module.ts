//список подключаемых библиотек
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';

import { LoginComponent } from './login/login.component';
import { RegistryComponent } from './registry/registry.component';
import { BookListComponent } from './booklist/booklist.component';
import { MyBookListComponent } from './mybooklist/mybooklist.component';

import { ToastrComponent } from './toastr/toastr.component';
import { AuthGuard } from './shared/auth.guard';
import { CookieService } from 'ngx-cookie-service';


@NgModule({
  //классы представлений
  declarations: [
    AppComponent,
    NavMenuComponent,

    LoginComponent,
    RegistryComponent,
    BookListComponent,
    MyBookListComponent,

    ToastrComponent
  ],
  //модули для работы классов из declarations
  imports: [
    //модуль для работы с браузером
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    //http клиент для обращения на сервер ASP.NET Core
    HttpClientModule,
    //модкль для работы с html
    FormsModule,
    RouterModule.forRoot([
      { path: '', redirectTo: 'login', pathMatch: 'full' },
      { path: 'login', component: LoginComponent },
      { path: 'registry', component: RegistryComponent },
      { path: 'booklist', component: BookListComponent },
      { path: 'mybooklist', component: MyBookListComponent, canActivate: [AuthGuard] },
    ], { relativeLinkResolution: 'legacy' })
  ],
  providers: [CookieService],
  //корневой компонент, вызывается по умолчанию при загрузке
  bootstrap: [AppComponent]
})
export class AppModule { }
