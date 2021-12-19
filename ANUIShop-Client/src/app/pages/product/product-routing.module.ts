import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductDetailComponent } from './product-detail/product-detail.component';
import { ProductComponent } from './product.component';

const routes: Routes = [
  {
    path: '',
    data: {
      title: 'Sản phẩm'
    },
    children: [
      {
        path: '',
        component: ProductComponent,
        data: {
          title: 'Danh sách'
        },
      },
      {
        path: 'them-moi',
        component: ProductDetailComponent,
        data: {
          title: 'Thêm mới'
        },
      },
      {
        path: 'cap-nhat/:id',
        component: ProductDetailComponent,
        data: {
          title: 'Cập nhật'
        },
      }
    ]
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProductRoutingModule { }
