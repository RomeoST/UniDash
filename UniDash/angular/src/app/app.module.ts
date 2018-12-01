import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';

import { LeftMenuComponent } from './main/menu/left-menu.component';
import { TopMenuComponent } from './main/header/top-menu.component';
import { RightContentComponent } from './main/content/right-content.component';
import { ListMenuComponent } from './main/menu/list-menu/list-menu.component';
import { FooterMenuComponent } from './main/menu/footer-menu/footer-menu.component';
import { AppRoutingModule } from './app-routing.module';
import { GlobalSharedModule } from './shared/global-shared.module';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './_guard/auth.guard';
import { AuthenticationService } from './shared/authentication.service';
import { SimpleNotificationsModule } from 'angular2-notifications';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { JwtInterceptor } from './_helpers/jwt.interceptor';
import { ErrorInterceptor } from './_helpers/error.interceptor';
import { NotificationsRSTService } from './shared/notifications-rst.service';

@NgModule({
  declarations: [
    AppComponent,
    LeftMenuComponent,
    TopMenuComponent,
    RightContentComponent,
    ListMenuComponent,
    FooterMenuComponent,
    LoginComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    GlobalSharedModule,
    SimpleNotificationsModule.forRoot()
  ],
  providers: [
    AuthGuard,
    AuthenticationService,
    NotificationsRSTService,
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
  ],
  bootstrap: [AppComponent],
})
export class AppModule{
 }
