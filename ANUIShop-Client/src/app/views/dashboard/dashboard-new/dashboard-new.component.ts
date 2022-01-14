import { Component, OnInit, TemplateRef } from '@angular/core';
import { ChartDataSets, ChartOptions, ChartType } from 'chart.js';
import { Label, SingleDataSet } from 'ng2-charts';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { OrderService } from '../../../services/order.service';
import { UtiltiesService } from '../../../services/utilties.service';

@Component({
  selector: 'app-dashboard-new',
  templateUrl: './dashboard-new.component.html',
  styleUrls: ['./dashboard-new.component.scss']
})
export class DashboardNewComponent implements OnInit {
  dataHeader: any = {};
  public barChartOptions: ChartOptions = {
    responsive: true,
  };
  public barChartLabels: Label[] = [];
  public barChartType: ChartType = 'bar';
  public barChartLegend = true;
  public barChartPlugins = [];

  public barChartData: ChartDataSets[] = [];

  // Pie
  public pieChartOptions: ChartOptions = {
    responsive: true,
  };
  public pieChartLabels: Label[] = [['Chờ xác nhận'], ['Xác nhận'], ['Đang giao'], ['Hoàn thành'], ['Hủy']];
  public pieChartData: SingleDataSet = [];
  public pieChartType: ChartType = 'pie';
  public pieChartLegend = true;
  public pieChartPlugins = [];
  public _backgroundColors:any[] = [{backgroundColor: ["#e84351", "#434a54", "#3ebf9b", "#4d86dc", "#f3af37"]}];

  firstDay: any = null;
  lastDay: any = null;

  constructor(
    private orderService: OrderService,
    private heplerService: UtiltiesService,
    private modalService: BsModalService,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    const dateNow = new Date();
    const date = new Date();
    date.setFullYear(date.getFullYear() - 1);
    this.NgayTu = this.heplerService.converDateTime(date, "yyyy-mm-dd");
    this.NgayDen = this.heplerService.converDateTime(dateNow, "yyyy-mm-dd");

    var d = new Date(), y = d.getFullYear(), m = d.getMonth();
    this.firstDay = this.heplerService.converDateTime(new Date(y, m, 1), "yyyy-mm-dd");
    this.lastDay = this.heplerService.converDateTime(new Date(y, m + 1, 0), "yyyy-mm-dd");

    this.loadDataHead();
    this.loadPie();
    this.loadTopProduct();
  }

  loadDataHead() {
    this.orderService.dheader()
      .subscribe(
        (res: any) => {
          this.dataHeader = res;
        },
        err => {
          console.error(err);
        }
      )
  }

  NgayTu: any = null;
  NgayDen: any = null;
  dataPie: any = {};
  tongdon: number = 0;
  loadPie() {
    this.orderService.dPie(this.NgayTu, this.NgayDen)
      .subscribe(
        (res: any) => {
          this.tongdon = res.All;
          let arr = [];
          const choXacNhan: any = parseFloat(((res.ChoXacNhan / res.All)*100).toFixed(2));
          const DangGiao: any = parseFloat(((res.DangGiao / res.All)*100).toFixed(2));
          const HoanThanh: any = parseFloat(((res.HoanThanh / res.All)*100).toFixed(2));
          const Huy: any = parseFloat(((res.Huy / res.All)*100).toFixed(2));
          const XacNhan: any = parseFloat(((res.XacNhan / res.All)*100).toFixed(2));

          arr.push(choXacNhan);
          arr.push(XacNhan);
          arr.push(DangGiao);
          arr.push(HoanThanh);
          arr.push(Huy);

          this.pieChartData = [...arr];
        },
        err => {
          console.error(err);
        }
      )
  }

  loadTopProduct() {
    this.orderService.topProduct(this.NgayTu, this.NgayDen)
      .subscribe(
        (res: any) => {
          debugger;
          this.barChartLabels = [];
          // this.barChartData = [
          //   { data: [10, 11, 15, 6, 25, 45, 30, 15, 6, 25, 45, 30], label: 'Đơn chờ xác nhận' },
          //   { data: [10, 11, 15, 6, 25, 45, 30, 15, 6, 25, 45, 30], label: 'Đơn xác nhận' },
          //   { data: [10, 11, 15, 6, 25, 45, 30, 15, 6, 25, 45, 30], label: 'Đơn đang giao' },
          //   { data: [10, 11, 15, 6, 25, 45, 30, 15, 6, 25, 45, 30], label: 'Đơn hoàn thành' },
          //   { data: [2, 0, 0, 4, 3, 0, 6, 15, 6, 25, 45, 30], label: 'Đơn hủy' }
          // ]
          var arr = [];
          res.map(x => {
            var obj = {data: [x.daban], label: x.ProductName};
            arr.push(obj);
            return arr;
          });
          this.barChartData = [...arr];
        }
      )
  }

  chartClicked(e) {
    debugger
  }

  clickHeader(status) {
 
  }

  modalRef: BsModalRef;
  openModelOrder(template: TemplateRef<any>, status) {
    this.status = status;
    this.loadOrder();
    this.modalRef = this.modalService.show(template, {
      animated: true,
      backdrop: 'static',
      class: 'modal-xl'
    });
  }

  dataSource: any = {};
  Page: any = 1;
  Limit: any = 10;
  Keyword: any = "";
  totalPage: any = 0;
  status: any = null
  sort: any = "";
  userID: any = 0;
  statusArr: any[] = [
    {id: 0, name: "Chờ xác nhận"},
    {id: 1, name: "Xác nhận"},
    {id: 2, name: "Đang giao"},
    {id: 3, name: "Hoàn thành"},
    {id: 4, name: "Chờ hủy"},
    {id: 5, name: "Hủy"},
  ]
  loadOrder() {
    this.orderService.getpaging(this.Limit, this.Page, this.status, this.firstDay, this.lastDay, this.sort, this.userID, this.Keyword)
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
      this.loadOrder();
    }
  }

  changeStatus(item) {
    var temp = 0;
    if(item.Status == 4) {
      temp = 5;
    }
    else if(item.Status == 0) {
      temp = 1;
    }
    this.orderService.updateStatus(item.Id, temp)
    .subscribe(
      (res: any) => {
        this.toastr.success("Thay đổi trạng thái thành công");
        this.loadOrder();
        this.loadDataHead();
        this.loadPie();
      },
      err => {
        console.error(err);
      }
    )
  }
}
