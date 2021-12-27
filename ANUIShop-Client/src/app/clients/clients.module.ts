import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ClientsRoutingModule } from './clients-routing.module';
import { HomeComponent } from './home/home.component';
import { ToastrModule } from 'ngx-toastr';
import { ModalModule } from 'ngx-bootstrap/modal';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CheckoutComponent } from './checkout/checkout.component';


@NgModule({
  declarations: [
    HomeComponent,
    CheckoutComponent
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
