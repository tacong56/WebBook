import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ImportStorageRoutingModule } from './import-storage-routing.module';
import { ImportStorageComponent } from './import-storage.component';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ModalModule } from 'ngx-bootstrap/modal';
import { ToastrModule } from 'ngx-toastr';
import { ImportStorageDetailComponent } from './import-storage-detail/import-storage-detail.component';
import { SharePipeModule } from '../../components/pipe/share-pipe.module';


@NgModule({
  declarations: [
    ImportStorageComponent,
    ImportStorageDetailComponent
  ],
  imports: [
    CommonModule,
    CKEditorModule,
    FormsModule,
    ReactiveFormsModule,
    ModalModule.forRoot(),
    ToastrModule,
    SharePipeModule,
    ImportStorageRoutingModule
  ]
})
export class ImportStorageModule { }
