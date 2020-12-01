import { Component } from '@angular/core';
import { ToastrService, ToastrModel } from './toastr.service'

@Component({
  selector: 'app-toastr',
  templateUrl: './toastr.component.html',
  styleUrls: ['./toastr.component.css']
})
export class ToastrComponent {

  notifications: Set<ToastrModel> = new Set<ToastrModel>();

  constructor(private _notificationService: ToastrService) {
    this._notificationService.getNotification()
      .subscribe((notification: ToastrModel) => {
        this.notifications.add(notification);
        setTimeout(() => {
          this.closeNotification(notification);
        }, 5000);
      });
  }

  public closeNotification(notification: ToastrModel) {
    this.notifications.delete(notification);
  }

}


