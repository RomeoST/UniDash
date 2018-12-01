import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AuthenticationService } from '../shared/authentication.service';
import { LoginModel } from '../shared/models/loginModel';
import { Router } from '@angular/router';
import { NotificationsRSTService } from '../shared/notifications-rst.service';
import { RegistrationModel } from '../shared/models/registrationModel';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  toogleSignUp = false;

  loginForm : FormGroup;
  regForm : FormGroup;
  

  constructor(formBuilder : FormBuilder, private _authService: AuthenticationService, private _notification : NotificationsRSTService, private _router : Router) {
    this.loginForm = formBuilder.group({
      userName: ["",Validators.required],
      password: ["",Validators.required],
    });
    this.regForm = formBuilder.group({
      fullName: ["", Validators.required],
      userName: ["", Validators.required],
      email: ["", [Validators.required, Validators.email]],
      password: ["", Validators.required]
    });
   }

  ngOnInit() {
  }

  login() {
    if (!this.loginForm.valid) {
      this._notification.warn("Увага", "Не всі поля заповнені!");
      return;
    }
    let model = {...this.loginForm.getRawValue()} as LoginModel;

    this._authService.login(model).subscribe(result => {
      if(result) {
        this._notification.success("Успіх", "Ви успішно авторизувались!");
        this._router.navigate(['/news']);
        window.location.reload();
        return;
      } else {
        this._notification.error("Помилка", "Логін або пароль введені невірно!");
      }
    });
  }

  register() {
    if (!this.regForm.valid) {
      this._notification.warn("Увага", "Не всі поля заповнені!");
      return;
    }

    let model = {...this.regForm.getRawValue()} as RegistrationModel;

    this._authService.registration(model).subscribe(result => {
      if(result) {
        this._notification.success("Успіх", "Ви успішно зареєструвались!");
        this._router.navigate(['/news']);
        window.location.reload();
        return;
      } else {
        this._notification.error("Помилка", "Ooops!");
      }
    });
  }

}
