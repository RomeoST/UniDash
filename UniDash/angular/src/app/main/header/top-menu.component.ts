import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from 'src/app/shared/authentication.service';

@Component({
  selector: 'app-top-menu',
  templateUrl: './top-menu.component.html',
  styleUrls: ['./top-menu.component.css']
})
export class TopMenuComponent implements OnInit {

  userName: string;
  
  constructor(private _authService: AuthenticationService) { }

  ngOnInit() {
    this.userName = JSON.parse(localStorage.getItem('currentUser')).user.userName;
  }

  logout(){
    this._authService.logout();
  }

}
