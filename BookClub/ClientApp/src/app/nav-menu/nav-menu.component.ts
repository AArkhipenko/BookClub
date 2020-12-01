import { Component } from '@angular/core';
import { AuthService } from '../shared/auth.service';
import { LogoutService } from '../logout/logout.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;

  constructor(private _authService: AuthService,
    private _logout: LogoutService) {
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
