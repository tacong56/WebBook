import { DatePipe } from '@angular/common';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { OrderService } from '../../services/order.service';
import { UtiltiesService } from '../../services/utilties.service';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.scss']
})
export class OrderComponent implements OnInit {

  modalRefDelete: BsModalRef;
  modalRefEdit: BsModalRef;
  dataSource: any = {};
  Page: any = 1;
  Limit: any = 1;
  Keyword: any = "";
  NgayTu: any = null;
  NgayDen: any = null;
  totalPage: any = 0;
  sort: any = "";
  userID: any = 0;

  constructor(
    private orderService: OrderService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private datePipe: DatePipe,
    private heplerService: UtiltiesService
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
    this.orderService.getpaging(this.Limit, this.Page, this.sort, this.userID, this.Keyword)
      .subscribe(
        (res: any) => {

          this.dataSource = res;
          this.totalPage = Math.ceil(Math.ceil(this.dataSource.totalRecord / this.dataSource.limit));
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

  itemSelected: any;
  openModalDelete(template: TemplateRef<any>, item) {
    this.itemSelected = item != null ? item : {};
    this.modalRefDelete = this.modalService.show(template, {
      animated: true,
      backdrop: 'static'
    });
  }

  openModalEdit(template: TemplateRef<any>, item) {
    this.itemSelected = item != null ? item : {};
    this.modalRefEdit = this.modalService.show(template, {
      animated: true,
      backdrop: 'static'
    });
  }

  saveChangeSatatus(status) {
    this.orderService.updateStatus(this.itemSelected.Id, status)
      .subscribe(
        (res: any) => {
          this.toastr.success("Thay đổi trạng thái thành công");
          console.log(res);
        },
        err => {
          console.error(err);
        }
      )
  }

  closeChangeStatus() {
    this.modalRefEdit.hide();
    this.load();
  }

  deleteItem() {
    this.orderService.delete(this.itemSelected.productId)
    .subscribe(
      (res: any) => {
        if(res == 1) this.toastr.success("Xóa thành công.");
        else this.toastr.error("Xóa không thành công.");
        this.modalRefDelete.hide();
        this.load();
      },
      err => {
        console.error(err)
      }
    )
  }

  filter() {
    this.load();
  }
}
