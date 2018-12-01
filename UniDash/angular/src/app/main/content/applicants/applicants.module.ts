import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

// Components
import { ApplicantTableComponent } from './applicant-table/applicant-table.component';
import { ApplicantEditComponent } from './applicant-edit/applicant-edit.component';
import { ApplicantFilterComponent } from './applicant-filter/applicant-filter.component';
import { ApplicantsRoutingModule } from './applicants-routing.module';
import { GlobalSharedModule } from '../../../shared/global-shared.module';
import { ApplicantManagerService } from './shared/applicant-manager.service';
import { SimpleNotificationsModule } from 'angular2-notifications';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { JwtInterceptor } from 'src/app/_helpers/jwt.interceptor';
import { ErrorInterceptor } from 'src/app/_helpers/error.interceptor';
import { NotificationsRSTService } from 'src/app/shared/notifications-rst.service';

@NgModule({
  imports: [
    ApplicantsRoutingModule,
    GlobalSharedModule,
    SimpleNotificationsModule.forRoot()
  ],
  declarations: [ 
    ApplicantTableComponent, 
    ApplicantEditComponent, 
    ApplicantFilterComponent ],
    providers: [
      ApplicantManagerService,
      NotificationsRSTService,
      { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
      { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    ]
})
export class ApplicantsModule { }
