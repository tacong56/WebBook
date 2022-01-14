import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TransactionComponent } from './transaction.component';

const routes: Routes = [
  {
    path: "",
    data: {
      title: "Thanh toán"
    },
    children: [
      {
        path: "",
        component: TransactionComponent,
        data: {
          title: "Danh sách"
        }
      },
      // {
      //   path: "them-moi",
      //   component: OrderDetailComponent,
      //   data: {
      //     title: "Thêm mới"
      //   }
      // },
      // {
      //   path: "cap-nhat/:id",
      //   component: OrderDetailComponent,
      //   data: {
      //     title: "Cập nhật"
      //   }
      // }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TransactionRoutingModule { }
