import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Subject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';

@Injectable()
export class AuthenticationService {

  constructor(private _http : HttpClient) { }

  public isUserLoggedIn : Subject<boolean> = new Subject<boolean>();

  login(userName : string, password : string){
    return this._http.post<any>(`${environment.API_URL}/api/account/login`, {UserName : userName, Password : password})
      .pipe(map(user => {
        if(user && user.token){
          localStorage.setItem('currentUser', JSON.stringify(user));
          this.isUserLoggedIn.next(true);
        }
        return true;
      }));
  }

  logout(){
    localStorage.removeItem('currentItem');
    this.isUserLoggedIn.next(false);
  }
}
