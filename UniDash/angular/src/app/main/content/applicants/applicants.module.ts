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
    providers: [ApplicantManagerService]
})
export class ApplicantsModule { }
