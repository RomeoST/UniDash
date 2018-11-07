import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';

import { LeftMenuComponent } from './menu/left-menu.component';
import { TopMenuComponent } from './top-menu/top-menu.component';
import { RightContentComponent } from './content/right-content.component';
import { ListMenuComponent } from './menu/list-menu/list-menu.component';
import { FooterMenuComponent } from './menu/footer-menu/footer-menu.component';
import { NewsComponent } from './content/news/news.component';
import { ApplicantsModule } from './content/applicants/applicants.module';
import { AppRoutingModule } from './app-routing.module';
import { GlobalSharedModule } from './shared/global-shared.module';
import { NewsModule } from './content/news/news.module';

@NgModule({
  declarations: [
    AppComponent,
    LeftMenuComponent,
    TopMenuComponent,
    RightContentComponent,
    ListMenuComponent,
    FooterMenuComponent,
    NewsComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    ApplicantsModule,
    NewsModule,
    GlobalSharedModule
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule{
 }
