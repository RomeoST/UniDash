import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { Http, Response } from '@angular/http'
// rx
import { map, catchError, toArray  } from 'rxjs/operators';
import { throwError, of } from "rxjs";

import { NotificationsService } from 'angular2-notifications';
import { AuthenticationService } from './shared/authentication.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  isAuth : boolean = false;

  constructor(private _authService : AuthenticationService ,private _notifications: NotificationsService){
    this._authService.isUserLoggedIn.subscribe(p => {
      this.isAuth = p;
    })
  }

  ngOnInit(){
  }
}
