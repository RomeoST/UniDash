import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { IApplicantModel, ApplicantModel } from '../shared/ApplicantModel';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ApplicantManagerService } from '../shared/applicant-manager.service';
import { MatSelectionList, MatSelectionListChange } from '@angular/material';
import { NotificationsService } from 'angular2-notifications';
import { SpecialityModel } from 'src/app/shared/models/specialityModel';
import { OperationDetails } from 'src/app/shared/models/OperationDetails';

@Component({
  selector: 'app-applicant-edit',
  templateUrl: './applicant-edit.component.html',
  styleUrls: ['./applicant-edit.component.css']
})
export class ApplicantEditComponent implements OnInit {

  applicantsList : IApplicantModel[] = []; // left list of the applicants
  specialityList : SpecialityModel[] = [];
  applicantForm : FormGroup;
  applicantManager : ApplicantManagerService;

  @ViewChild('list') listElements : MatSelectionList;
  selectedApplicant : IApplicantModel;

  filterShown = false; // для отображения панели поиска
  multiSelect = false;
  newApplicantMode = false;

  constructor(formBuilder : FormBuilder, applicantManager : ApplicantManagerService, private _notifications: NotificationsService) {
    this.applicantForm = formBuilder.group({
      "applicantId" : [{value:"", disabled:true}],
      "nameApplicant" : [{value:"", disabled : true}, Validators.required],
      "phoneApplicant" : [{value:"", disabled : true}, Validators.required],
      "schoolCollege" : [{value:"", disabled : true}, Validators.required],
      "address" : [{value:"", disabled : true}, Validators.required],
      "mailApplicant" : [{value:"", disabled : true}, Validators.email],
      "speciality" : [{value:""}],
      "specialityList" : [{value:"", disabled : true}, Validators.required],
      "mark" : [{value:"", disabled: true}],
      "nameFound" :[{value:"", disabled: true}, Validators.required],
      "nameAdded" : [{value:"", disabled: true}],
      "dateEdit" :[{value:"", disabled: true}],
      "dateAdd" : [{value:"", disabled: true}]
    }); 
    this.applicantManager = applicantManager;
   }

  ngOnInit() {
    this.applicantManager.getApplicantList().subscribe((p : IApplicantModel[]) => {
      for(let i = 0; i < p.length; i++){
        var dig = p[i].speciality.split(' ');
        this.applicantsList[i] = {...p[i]};
        this.applicantsList[i].specialityList = dig.map(Number);
      }
    });
    this.applicantManager.getSpecialityList().subscribe((p : SpecialityModel[]) =>{
      for (let i = 0; i < p.length; i++) {
        this.specialityList[i] = {...p[i]};
      }
    });
  }

  selectApplicant(applicant : ApplicantModel, event : MouseEvent){
    if(!this.applicantForm.pristine || this.newApplicantMode) return;
    
    for(var key in applicant){
      if(this.applicantForm.controls[key] !== undefined){
        this.applicantForm.controls[key].setValue(applicant[key]);
        switch (key) {
          case "applicantId":
          case "nameAdded":
          case "dateEdit":
          case "dateAdd":
            continue;
        }
        this.applicantForm.controls[key].enable();
      }
    }
    this.multiSelect = event == null ? false : event.ctrlKey;
  }

  // event when you clicked on the list
  onNgModelChange(event : MatSelectionListChange) {
    this.selectedApplicant = event.option.value;
     if(!this.multiSelect){
      event.source.deselectAll();
      event.option._setSelected(true); 
    }     
}

submit(event){
  switch(event){
    case 'save' : this.saveApplicant(); break;
    case 'cancel' : this.cancelApplicant(); break;
  }
}

saveApplicant(){
  let model = {...this.applicantForm.getRawValue()};
  model.speciality = model.specialityList.join(' ');
  model.dateAdd = '2018-01-01T00:00:00';
  model.dateEdit = '2018-01-01T00:00:00';
  model.selected = true;

  this.applicantManager.saveApplicant(model).subscribe((p : OperationDetails) => {
    let t = { timeOut: 5000,showProgressBar: true, pauseOnHover: true, clickToClose: true, animate: 'fromRight'}
    if(!p.success){
      // TODO: Создать сервис нотификаций
      this._notifications.error(p.message, p.object.join('\n') ,t);
      return;
    }
    
    if(p.object.hasOwnProperty('applicant'))
    {
      let respApplicant = p.object.applicant as ApplicantModel;
      this.applicantsList[0] = {...respApplicant, selected : true};
      this.applicantForm.patchValue({applicantId : respApplicant.applicantId, dateAdd : respApplicant.dateAdd, dateEdit : respApplicant.dateEdit});
    }

    this._notifications.success("Інформація", "Абітурієнт збережений");
    this.applicantForm.markAsPristine();
    this.newApplicantMode = false;
  });
}

cancelApplicant(){
  this.applicantsList.shift();
  this.applicantForm.markAsPristine();
  this.applicantForm.disable();
  this.newApplicantMode = false;
}

addApplicant(){
  if(!this.applicantForm.pristine || this.newApplicantMode){
    let t = {
      timeOut: 5000,showProgressBar: true, pauseOnHover: true, clickToClose: true, animate: 'fromRight'
    }
    this._notifications.warn("Увага", "У вас є не збережені дані! Щоб продовжити, натисніть 'Відміна'", t);
    return;
  }

  this.listElements.deselectAll();

  let newApplicant = ApplicantModel.newApplicant();
  this.applicantsList.unshift(newApplicant);

  this.selectApplicant(newApplicant, null);
  this.newApplicantMode = true;
}

removeApplicant(){
  if(this.selectedApplicant == null){
    let t = {
      timeOut: 5000,showProgressBar: true,pauseOnHover: true,  clickToClose: true, animate: 'fromBottom'
    }
    this._notifications.warn("Увага", "Елемент для видалення не обрано!", t);
    return;
  }
  this.applicantManager.removeApplicant(this.selectedApplicant);
  this.applicantForm.disable();
  this.applicantForm.reset();
}

}
