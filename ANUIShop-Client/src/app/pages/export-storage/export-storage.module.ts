import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ExportStorageRoutingModule } from './export-storage-routing.module';
import { ToastrModule } from 'ngx-toastr';
import { ModalModule } from 'ngx-bootstrap/modal';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { ExportStorageComponent } from './export-storage.component';
import { ExportStorageDetailComponent } from './export-storage-detail/export-storage-detail.component';


@NgModule({
  declarations: [
    ExportStorageComponent,
    ExportStorageDetailComponent
  ],
  imports: [
    CommonModule,
    CKEditorModule,
    FormsModule,
    ReactiveFormsModule,
    ModalModule.forRoot(),
    ToastrModule,
    ExportStorageRoutingModule
  ]
})
export class ExportStorageModule { }
