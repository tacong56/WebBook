import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PromotionRoutingModule } from './promotion-routing.module';
import { ToastrModule } from 'ngx-toastr';
import { ModalModule } from 'ngx-bootstrap/modal';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { PromotionComponent } from './promotion.component';
import { PromotionDetailComponent } from './promotion-detail/promotion-detail.component';


@NgModule({
  declarations: [
    PromotionComponent,
    PromotionDetailComponent
  ],
  imports: [
    CommonModule,
    CKEditorModule,
    FormsModule,
    ReactiveFormsModule,
    ModalModule.forRoot(),
    ToastrModule,
    PromotionRoutingModule
  ]
})
export class PromotionModule { }
