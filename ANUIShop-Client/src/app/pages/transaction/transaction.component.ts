import { Component, OnInit } from '@angular/core';
import { OrderService } from '../../services/order.service';
import { UtiltiesService } from '../../services/utilties.service';

@Component({
  selector: 'app-transaction',
  templateUrl: './transaction.component.html',
  styleUrls: ['./transaction.component.scss']
})
export class TransactionComponent implements OnInit {

  dataSource: any = {};
  Page: any = 1;
  Limit: any = 10;
  NgayTu: any = null;
  NgayDen: any = null;
  totalPage: any = 0;
  sort: any = "";

  constructor(
    private heplerService: UtiltiesService,
    private orderService: OrderService
  ) { }

  ngOnInit(): void {
    const dateNow = new Date();
    const date = new Date();
    date.setFullYear(date.getFullYear() - 1);
    this.NgayTu = this.heplerService.converDateTime(date, "yyyy-mm-dd");
    this.NgayDen =this.heplerService.converDateTime(dateNow, "yyyy-mm-dd");
    console.log(this.NgayTu);
    this.load();
  }

  load() {
    this.orderService.getpagingtran(this.Limit, this.Page, this.NgayTu, this.NgayDen, this.sort)
      .subscribe(
        (res: any) => {

          this.dataSource = res;
          this.totalPage = Math.ceil(Math.ceil(this.dataSource.TotalRecord / this.dataSource.Limit));
          this.setListPaging();
        },
        err => {
          console.error(err);
        }
      )
  }

  listNumberPaging: any[] = [];
  setListPaging() {
    let arr = []
    for(let i = 0; i < this.totalPage; i++) {
      arr.push(i+1);
    }
    this.listNumberPaging = [...arr];
  }

  onClickPaging(page) {
    if(page != this.Page) {
      this.Page = page;
      this.load();
    }
  }

  filter() {
    this.load();
  }
}
