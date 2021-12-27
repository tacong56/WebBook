import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { OrderDetailComponent } from './order-detail/order-detail.component';
import { OrderComponent } from './order.component';

const routes: Routes = [
  {
    path: "",
    data: {
      title: "Đơn hàng"
    },
    children: [
      {
        path: "",
        component: OrderComponent,
        data: {
          title: "Danh sách"
        }
      },
      {
        path: "them-moi",
        component: OrderDetailComponent,
        data: {
          title: "Thêm mới"
        }
      },
      {
        path: "cap-nhat/:id",
        component: OrderDetailComponent,
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
export class OrderRoutingModule { }
