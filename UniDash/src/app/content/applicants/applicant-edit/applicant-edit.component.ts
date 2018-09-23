import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { ApplicantModel } from '../shared/ApplicantModel';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ApplicantManagerService } from '../shared/applicant-manager.service';
import { MatSelectionList, MatSelectionListChange } from '@angular/material';
import { NotificationsService } from 'angular2-notifications';

@Component({
  selector: 'app-applicant-edit',
  templateUrl: './applicant-edit.component.html',
  styleUrls: ['./applicant-edit.component.css']
})
export class ApplicantEditComponent implements OnInit {

  applicantsList : ApplicantModel[] = []; // left list of the applicants
  specialityList : {id : number, name : string}[];
  applicantForm : FormGroup;
  applicantManager : ApplicantManagerService;

  @ViewChild('list') listElements : MatSelectionList;
  selectedApplicant : ApplicantModel;

  filterShown = false; // для отображения панели поиска
  multiSelect = false;
  newApplicantMode = false;

  constructor(formBuilder : FormBuilder, applicantManager : ApplicantManagerService, private _notifications: NotificationsService) {
    this.applicantForm = formBuilder.group({
      "ApplicantId" : [{value:"", disabled:true}],
      "ApplicantName" : [{value:"", disabled : true}, Validators.required],
      "ApplicantPhone" : [{value:"", disabled : true}, Validators.required],
      "SchoolCollege" : [{value:"", disabled : true}, Validators.required],
      "Address" : [{value:"", disabled : true}, Validators.required],
      "ApplicantMail" : [{value:"", disabled : true}, Validators.email],
      "Speciality" : [{value:"", disabled : true}, Validators.required],
      "Mark" : [{value:"", disabled: true}],
      "NameFound" :[{value:"", disabled: true}, Validators.required],
      "NameAdded" : [{value:"", disabled: true}],
      "DateEdit" :[{value:"", disabled: true}],
      "DateAdd" : [{value:"", disabled: true}]
    }); 
    this.applicantManager = applicantManager;
   }

  ngOnInit() {
    this.applicantsList = this.applicantManager.getApplicantList().reverse();
    this.specialityList = this.applicantManager.getSpecialityList();
  }

  selectApplicant(applicant : any, event : MouseEvent){
    if(!this.applicantForm.pristine || this.newApplicantMode) return;
    
    for(var key in applicant){
      if(this.applicantForm.controls[key] !== undefined){
        this.applicantForm.controls[key].setValue(applicant[key]);
        switch (key) {
          case "ApplicantId":
          case "NameAdded":
          case "DateEdit":
          case "DateAdd":
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

  saveApplicant(){
    this.applicantManager.saveApplicant({...this.applicantForm.getRawValue(), selected : true});
    this.applicantForm.markAsPristine();
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
    let newApplicant = this.applicantManager.addApplicant();
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
