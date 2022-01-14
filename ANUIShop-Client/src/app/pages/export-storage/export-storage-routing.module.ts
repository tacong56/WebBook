import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ExportStorageDetailComponent } from './export-storage-detail/export-storage-detail.component';
import { ExportStorageComponent } from './export-storage.component';

const routes: Routes = [
  {
    path: "",
    data: {
      title: "Phiếu xuất"
    },
    children: [
      {
        path: "",
        component: ExportStorageComponent,
        data: {
          title: "Danh sách"
        }
      },
      {
        path: "them-moi",
        component: ExportStorageDetailComponent,
        data: {
          title: "Thêm mới"
        }
      },
      {
        path: "cap-nhat/:id",
        component: ExportStorageDetailComponent,
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
export class ExportStorageRoutingModule { }
