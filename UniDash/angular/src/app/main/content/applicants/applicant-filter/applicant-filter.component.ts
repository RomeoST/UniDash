import { Component, OnInit, Input } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-applicant-filter',
  templateUrl: './applicant-filter.component.html',
  styleUrls: ['./applicant-filter.component.css']
})
export class ApplicantFilterComponent implements OnInit {

  filterGroup : FormGroup;

  @Input() spec = [];

  constructor(formBuild : FormBuilder) { 
    this.filterGroup = formBuild.group({
      "id" : [""],
      "fullName" : [""],
      "phone" : [""],
      "email" : [""],
      "speciality" : [""]
    });
  }

  ngOnInit() {
  }

}
