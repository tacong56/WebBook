import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProductRoutingModule } from './product-routing.module';
import { ProductComponent } from './product.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ProductDetailComponent } from './product-detail/product-detail.component';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { ToastrModule } from 'ngx-toastr';
import { ModalModule } from 'ngx-bootstrap/modal';
import { SharePipeModule } from '../../components/pipe/share-pipe.module';
// import { NgxDialogsModule } from 'ngx-dialogs';


@NgModule({
  declarations: [
    ProductComponent,
    ProductDetailComponent,
  ],
  imports: [
    CommonModule,
    CKEditorModule,
    FormsModule,
    ReactiveFormsModule,
    ProductRoutingModule,
    ModalModule.forRoot(),
    SharePipeModule,
    ToastrModule,
    // NgxDialogsModule
  ]
})
export class ProductModule { }
