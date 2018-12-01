import { Injectable } from '@angular/core';
import { NotificationsService } from 'angular2-notifications';

@Injectable({
  providedIn: 'root'
})
export class NotificationsRSTService {

  constructor(private _notification : NotificationsService) { }

  private property = { timeOut: 5000,showProgressBar: true, pauseOnHover: true, clickToClose: true, animate: 'fromRight'};

  success(title : string, message : string){
    this._notification.success(title, message, this.property);
  }

  error(title : string, message : string){
    this._notification.error(title, message, this.property);
  }

  warn(title : string, message : string){
    this._notification.warn(title, message, this.property);
  }
}
