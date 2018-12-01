import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Subject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';
import { LoginModel } from './models/LoginModel';
import { RegistrationModel } from './models/registrationModel';
import { OperationDetails } from './models/OperationDetails';

@Injectable()
export class AuthenticationService {

  constructor(private _http : HttpClient) { }

  public isUserLoggedIn : Subject<boolean> = new Subject<boolean>();

  login(model: LoginModel){
    return this._http.post<OperationDetails>('api/account/login', {UserName: model.userName, Password: model.password})
      .pipe(map(result => {
        if(result && result.success){
          localStorage.setItem('currentUser', JSON.stringify(result.object));
          this.isUserLoggedIn.next(true);
          return true;
        }
        return false;
      }));
  }

  logout(){
    localStorage.removeItem('currentUser');
    this.isUserLoggedIn.next(false);
    window.location.reload();
  }

  registration(model: RegistrationModel){
    return this._http.post<OperationDetails>(`${environment.API_URL}/api/account/register`, model)
      .pipe(map(result => {
        if(result && result.success) {
          localStorage.setItem('currentUser', JSON.stringify(result.object));
          this.isUserLoggedIn.next(true);
          return true;
        }
        return false;
      }))
  }
}
