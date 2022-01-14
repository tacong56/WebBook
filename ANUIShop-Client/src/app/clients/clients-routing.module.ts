import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CheckoutDetailComponent } from './checkout/checkout-detail/checkout-detail.component';
import { CheckoutComponent } from './checkout/checkout.component';
import { HomeComponent } from './home/home.component';
import { PageProductHotComponent } from './page-product-hot/page-product-hot.component';
import { PageProductComponent } from './page-product/page-product.component';

const routes: Routes = [
  { path: "trang-chu", component: HomeComponent },
  { path: "thanh-toan", component: CheckoutComponent },
  { path: "notify-payment", component: CheckoutDetailComponent },
  { path: "sach-theo-danh-muc/:id", component: PageProductComponent },
  { path: "tim-kiem-sach", component: PageProductHotComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ClientsRoutingModule { }
