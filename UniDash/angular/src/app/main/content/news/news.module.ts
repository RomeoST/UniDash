import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { NewsRoutingModule } from './news-routing.module';
import { GlobalSharedModule } from '../../../shared/global-shared.module';
import { NewsComponent } from './news.component';

@NgModule({
  imports: [
    GlobalSharedModule,
    NewsRoutingModule
  ],
  declarations: [NewsComponent]
})
export class NewsModule { }
