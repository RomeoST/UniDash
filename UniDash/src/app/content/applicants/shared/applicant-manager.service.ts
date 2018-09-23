import { Injectable } from '@angular/core';
import { ApplicantModel } from './ApplicantModel';

@Injectable({
  providedIn: 'root'
})
export class ApplicantManagerService {

  private testList : ApplicantModel[] =[
    new ApplicantModel('1',"test1","ahe@gmail.com","669966506","DUT","Kyiv",[1],"","Test2","Test3","10.10.2018 18:20","10.10.2018 18:20"),
    new ApplicantModel('2',"test1","ahe@gmail.com","669966506","DUT","Kyiv",[2],"","Test2","Test3","10.10.2018 18:20","10.10.2018 18:20")
  ];

  getApplicantList() : ApplicantModel[] {
    return this.testList;
  }

  getApplicantTable(){

  }

  getSpecialityList() : {id : number, name : string}[]{
    return [{id : 1, name : "test"}, {id : 2, name: "test2"}];
  }

  addApplicant() : ApplicantModel{
    let newModel = new ApplicantModel("0*", "Новий абітурієнт");
    newModel.selected = true;
    this.testList.unshift(newModel);

    return newModel;
  }

  removeApplicant(model : ApplicantModel){
    var idx = this.testList.indexOf(model,0);
    if(idx > -1){
      this.testList.splice(idx,1);
    }
  }

  saveApplicant(model : ApplicantModel){
    var id = this.testList.findIndex(p => p.ApplicantId == model.ApplicantId);
    if(id !== -1){
      this.testList[id] = model;
      this.testList[id].selected = true;
      if(model.ApplicantId === "0*")
      {
        if(this.testList.length > 1){
          var lastIdx = this.testList[1].ApplicantId;
          this.testList[id].ApplicantId = +lastIdx + 1 + '';
        }
        else{
          this.testList[id].ApplicantId = '1';
        }
      }
    }
  }

}
