import { NgModule } from '@angular/core';
import { Routes, RouterModule, PreloadAllModules } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './_guard/auth.guard';
import { ApplicantsModule } from './main/content/applicants/applicants.module';
import { NewsModule } from './main/content/news/news.module';

const routes: Routes = [
  { path: "login", component: LoginComponent, pathMatch: "full"},
  { path: '', canActivate: [AuthGuard], children:[
    { path: 'applicants', loadChildren: () => ApplicantsModule  },
    { path: '', loadChildren: () => NewsModule },
    { path: '**', loadChildren: () => NewsModule }
  ]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {
    preloadingStrategy: PreloadAllModules
  })],
  exports: [RouterModule]
})
export class AppRoutingModule { }