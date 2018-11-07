import { Injectable } from '@angular/core';
import { IApplicantModel } from './ApplicantModel';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators'
import { SpecialityModel } from 'src/app/shared/models/specialityModel';

@Injectable({
  providedIn: 'root'
})
export class ApplicantManagerService {

  private testList : IApplicantModel[] =[
    /*new ApplicantModel('1',"test1","ahe@gmail.com","669966506","DUT","Kyiv",[1],"","Test2","Test3","10.10.2018 18:20","10.10.2018 18:20"),
    new ApplicantModel('2',"test1","ahe@gmail.com","669966506","DUT","Kyiv",[2],"","Test2","Test3","10.10.2018 18:20","10.10.2018 18:20")*/
  ];

  constructor(private _http : HttpClient){}

  getApplicantList() {
    return this._http.get<IApplicantModel[]>('api/applicant/list');
  }

  getApplicantTable(){

  }

  getSpecialityList() {
    return this._http.get<SpecialityModel[]>('api/applicant/specialties');
  }

  removeApplicant(model : IApplicantModel){
    var idx = this.testList.indexOf(model,0);
    if(idx > -1){
      this.testList.splice(idx,1);
    }
  }

  saveApplicant(model : IApplicantModel){
    return this._http.post('api/applicant/save', model);
  }

}
