import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProductRoutingModule } from './product-routing.module';
import { ProductComponent } from './product.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ProductDetailComponent } from './product-detail/product-detail.component';
import { NumberPipePipe } from '../../components/pipe/number-pipe.pipe';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { ToastrModule } from 'ngx-toastr';
import { ModalModule } from 'ngx-bootstrap/modal';
// import { NgxDialogsModule } from 'ngx-dialogs';


@NgModule({
  declarations: [
    ProductComponent,
    ProductDetailComponent,
    NumberPipePipe
  ],
  imports: [
    CommonModule,
    CKEditorModule,
    FormsModule,
    ReactiveFormsModule,
    ProductRoutingModule,
    ModalModule.forRoot(),
    ToastrModule,
    // NgxDialogsModule
  ]
})
export class ProductModule { }
