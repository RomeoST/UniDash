import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router, Route, CanLoad } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthenticationService } from '../shared/authentication.service';
@Injectable()
export class AuthGuard implements CanActivate, CanLoad {
  
  canLoad(route: Route): boolean | Observable<boolean> | Promise<boolean> {
    return this.checkAuth(null);
  }

  constructor(private router : Router, private authService: AuthenticationService){}

  canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    return this.checkAuth(state)
  }

  checkAuth(state){
    if(localStorage.getItem('currentUser')){
      this.authService.isUserLoggedIn.next(true);
      return true;
    }

    if(state != null) {
      this.router.navigate(['/login'], {queryParams: {returnUrl: state.url}});
    }
    else {
      this.router.navigateByUrl(location.pathname);
    }

    return true;
  }
}
