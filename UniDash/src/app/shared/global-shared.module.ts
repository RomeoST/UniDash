import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { SimpleNotificationsModule } from "angular2-notifications";
import { HttpModule } from "@angular/http";
import { ReactiveFormsModule } from "@angular/forms";
import { MatFormFieldModule, MatInputModule, MatButtonModule, MatProgressBarModule, MatListModule, MatSelectModule } from "@angular/material";

@NgModule({
    imports: [
        CommonModule,
    ],
    exports: [
        CommonModule,
        HttpModule,
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