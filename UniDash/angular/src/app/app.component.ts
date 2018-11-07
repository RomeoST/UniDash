import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { Http, Response } from '@angular/http'
// rx
import { map, catchError, toArray  } from 'rxjs/operators';
import { throwError, of } from "rxjs";

import { NotificationsService } from 'angular2-notifications';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  constructor(
    //private http : Http, 
    private _notifications: NotificationsService
    ){}

  ngOnInit(){
    
    /*let test = this.http.get('api/applicant/list').pipe(
      map(res => res.json())).subscribe(res => console.log(res));*/

  }
}
