import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { NewsRoutingModule } from './news-routing.module';
import { GlobalSharedModule } from '../../shared/global-shared.module';

@NgModule({
  imports: [
    GlobalSharedModule,
    NewsRoutingModule
  ],
  declarations: []
})
export class NewsModule { }
