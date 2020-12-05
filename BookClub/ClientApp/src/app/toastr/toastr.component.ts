import { Component } from '@angular/core';

import { ToastrService, ToastrModel } from './toastr.service'

@Component({
  selector: 'app-toastr',
  templateUrl: './toastr.component.html',
  styleUrls: ['./toastr.component.css']
})
export class ToastrComponent {

  toastrs: Set<ToastrModel> = new Set<ToastrModel>();
  timers: Map<ToastrModel, any> = new Map<ToastrModel, any>();

  constructor(private _toastrService: ToastrService) {
    this._toastrService.getNotification()
      .subscribe((model: ToastrModel) => {
        const timer = setTimeout(() => {
          this.closeToastr(model);
        }, 5000);
        this.toastrs.add(model);
        this.timers.set(model, timer);
      });
  }

  /**
   * Закрытие встплывающего окна
   * @param model - модель окна, которое закрывается
   */
  public closeToastr(model: ToastrModel) {
    this.toastrs.delete(model);
    this.timers.delete(model);
  }

  /**
   * При выводе мышки за пределы всплывающего окна
   * @param model - модель окна
   */
  public onMouseOut(model: ToastrModel) {
    if (!this.toastrs.has(model) ||
      !this.timers.has(model))
      return;

    const timer = setTimeout(() => {
      this.closeToastr(model);
    }, 5000);
    this.timers.set(model, timer);
  }

  /**
   * При вводе мышки в пределы всплывающего окна
   * @param model - модель окна
   */
  public onMouseOver(model: ToastrModel) {
    if (!this.toastrs.has(model) ||
      !this.timers.has(model))
      return;

    const timer = this.timers.get(model);
    clearTimeout(timer);
  }

}


