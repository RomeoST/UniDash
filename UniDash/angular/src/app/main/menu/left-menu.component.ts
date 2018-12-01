import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-left-menu',
  templateUrl: './left-menu.component.html',
  styleUrls: ['./left-menu.component.css']
})
export class LeftMenuComponent implements OnInit {

  userName: string;
  
  constructor() { }

  ngOnInit() {
    this.userName = JSON.parse(localStorage.getItem('currentUser')).user.userName;
  }

}
