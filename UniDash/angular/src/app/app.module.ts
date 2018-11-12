import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';

import { LeftMenuComponent } from './main/menu/left-menu.component';
import { TopMenuComponent } from './main/header/top-menu.component';
import { RightContentComponent } from './main/content/right-content.component';
import { ListMenuComponent } from './main/menu/list-menu/list-menu.component';
import { FooterMenuComponent } from './main/menu/footer-menu/footer-menu.component';
import { ApplicantsModule } from './main/content/applicants/applicants.module';
import { AppRoutingModule } from './app-routing.module';
import { GlobalSharedModule } from './shared/global-shared.module';
import { NewsModule } from './main/content/news/news.module';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './_guard/auth.guard';
import { AuthenticationService } from './shared/authentication.service';

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
    ApplicantsModule,
    GlobalSharedModule
  ],
  providers: [
    AuthGuard,
    AuthenticationService
  ],
  bootstrap: [AppComponent],
})
export class AppModule{
 }
