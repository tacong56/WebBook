import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ImportStorageDetailComponent } from './import-storage-detail/import-storage-detail.component';
import { ImportStorageComponent } from './import-storage.component';

const routes: Routes = [
  {
    path: "",
    data: {
      title: "Phiếu nhập"
    },
    children: [
      {
        path: "",
        component: ImportStorageComponent,
        data: {
          title: "Danh sách"
        }
      },
      {
        path: "them-moi",
        component: ImportStorageDetailComponent,
        data: {
          title: "Thêm mới"
        }
      },
      {
        path: "cap-nhat/:id",
        component: ImportStorageDetailComponent,
        data: {
          title: "Cập nhật"
        }
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ImportStorageRoutingModule { }
