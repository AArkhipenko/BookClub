import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ToastrService {

  private notifications: Subject<ToastrModel> = new Subject<ToastrModel>();

  public getNotification(): Subject<ToastrModel> {
    return this.notifications;
  }

  public showToast(info: ToastrModel) {
    this.notifications.next(info);
  }
}

export class ToastrModel {
  public class: string;
  public message: string;

  constructor(type: ToastrType, message: string) {
    switch (type) {
      case ToastrType.error:
        this.class = "toastr-error";
        break;
      case ToastrType.warning:
        this.class = "toastr-warning";
        break;
      case ToastrType.success:
        this.class = "toastr-success";
        break;
      case ToastrType.info:
        this.class = "toastr-info";
        break;
    }
    this.message = message;
  }
}

export enum ToastrType {
  error,
  warning,
  success,
  info
}
