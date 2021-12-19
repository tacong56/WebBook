import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CategoryRoutingModule } from './category-routing.module';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ToastrModule } from 'ngx-toastr';
import { CategoryComponent } from './category.component';
import { ModalModule } from 'ngx-bootstrap/modal';
import { CategoryDetailComponent } from './category-detail/category-detail.component';
import { ConfirmDialogComponent } from '../../confirm-dialog/confirm-dialog.component';
import { MessageService } from '../../services/message.service';


@NgModule({
  declarations: [
    CategoryComponent,
    CategoryDetailComponent,
    ConfirmDialogComponent
  ],
  imports: [
    CommonModule,
    CKEditorModule,
    FormsModule,
    ReactiveFormsModule,
    ToastrModule,
    ModalModule.forRoot(),
    CategoryRoutingModule
  ],
  providers: [MessageService]
})
export class CategoryModule { }
