import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ClientsRoutingModule } from './clients-routing.module';
import { HomeComponent } from './home/home.component';
import { ToastrModule } from 'ngx-toastr';
import { ModalModule } from 'ngx-bootstrap/modal';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CheckoutComponent } from './checkout/checkout.component';
import { CheckoutDetailComponent } from './checkout/checkout-detail/checkout-detail.component';
import { PageProductComponent } from './page-product/page-product.component';
import { PageProductHotComponent } from './page-product-hot/page-product-hot.component';


@NgModule({
  declarations: [
    HomeComponent,
    CheckoutComponent,
    CheckoutDetailComponent,
    PageProductComponent,
    PageProductHotComponent
  ],
  imports: [
    CommonModule,
    ToastrModule,
    ModalModule.forRoot(),
    FormsModule,
    ReactiveFormsModule,
    ClientsRoutingModule,
  ]
})
export class ClientsModule { }
