import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ApplicantEditComponent } from './applicant-edit/applicant-edit.component';
import { ApplicantTableComponent } from './applicant-table/applicant-table.component';

const routes: Routes = [
  { path: "applicants/edit", component: ApplicantEditComponent },
  { path: "applicants", component: ApplicantTableComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ApplicantsRoutingModule { }
