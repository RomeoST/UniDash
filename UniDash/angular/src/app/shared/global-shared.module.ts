import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { SimpleNotificationsModule } from "angular2-notifications";
import { ReactiveFormsModule } from "@angular/forms";
import { MatFormFieldModule, MatInputModule, MatButtonModule, MatProgressBarModule, MatListModule, MatSelectModule } from "@angular/material";
import { HttpClientModule } from "@angular/common/http";

@NgModule({
    imports: [
        CommonModule,
    ],
    exports: [
        CommonModule,
        HttpClientModule,
        ReactiveFormsModule,
        MatFormFieldModule,
        MatInputModule,
        MatButtonModule,
        MatProgressBarModule,
        MatListModule,
        MatSelectModule,
    ]
})
export class GlobalSharedModule{}