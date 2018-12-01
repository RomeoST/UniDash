import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ApplicantEditComponent } from './applicant-edit/applicant-edit.component';
import { ApplicantTableComponent } from './applicant-table/applicant-table.component';

const routes: Routes = [
  { path: 'edit', component: ApplicantEditComponent },
  { path: '', component: ApplicantTableComponent },
  { path: '**', redirectTo: '', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ApplicantsRoutingModule { }
