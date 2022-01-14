import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { OrderService } from '../../../services/order.service';

@Component({
  selector: 'app-checkout-detail',
  templateUrl: './checkout-detail.component.html',
  styleUrls: ['./checkout-detail.component.scss']
})
export class CheckoutDetailComponent implements OnInit {

  orderID: any = null;
  transactionID: any = null;
  msg: string = '';
  order: any = {};

  constructor(
    private route: ActivatedRoute,
    private orderService: OrderService
  ) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.orderID = params['orderID'];
      this.transactionID = params['transactionID'];
      this.msg = params['msg'];
      console.log(this.transactionID)
      this.getOrder();
    });
    
  }

  getOrder() {
    this.orderService.get(this.orderID)
      .subscribe(
        (res: any) => {
          this.order = res.Data;
          console.log(this.order)
        }
      )
  }

}
