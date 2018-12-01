import { Injectable } from '@angular/core';
import { IApplicantModel } from './ApplicantModel';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators'
import { SpecialityModel } from 'src/app/shared/models/specialityModel';

@Injectable({
  providedIn: 'root'
})
export class ApplicantManagerService {
  constructor(private _http : HttpClient){}

  getApplicantList() {
    return this._http.get<IApplicantModel[]>('api/applicant/list');
  }

  getApplicant(id){
    return this._http.get<IApplicantModel>(`api/applicant/select/${id}`).pipe(map(result =>{
      return result;
    }));
  }

  getApplicantTable(){

  }

  getSpecialityList() {
    return this._http.get<SpecialityModel[]>('api/applicant/specialties');
  }

  removeApplicant(model : IApplicantModel){
    return this._http.delete(`api/applicant/remove/${model.applicantId}`);
  }

  saveApplicant(model : IApplicantModel){
    return this._http.post('api/applicant/save', model);
  }

}
