import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PromotionDetailComponent } from './promotion-detail/promotion-detail.component';
import { PromotionComponent } from './promotion.component';

const routes: Routes = [
  {
    path: "",
    data: {
      title: "Khuyến mãi"
    },
    children: [
      {
        path: "",
        component: PromotionComponent,
        data: {
          title: "Danh sách"
        }
      },
      {
        path: "them-moi",
        component: PromotionDetailComponent,
        data: {
          title: "Thêm mới"
        }
      },
      {
        path: "cap-nhat/:id",
        component: PromotionDetailComponent,
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
export class PromotionRoutingModule { }
