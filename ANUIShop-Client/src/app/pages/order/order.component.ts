import { DatePipe } from '@angular/common';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
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
  Limit: any = 10;
  Keyword: any = "";
  NgayTu: any = null;
  NgayDen: any = null;
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

  constructor(
    private orderService: OrderService,
    private route: ActivatedRoute,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private datePipe: DatePipe,
    private heplerService: UtiltiesService,

  ) { }

  ngOnInit(): void {
    const dateNow = new Date();
    const date = new Date();
    date.setFullYear(date.getFullYear() - 1);
    this.NgayTu = this.heplerService.converDateTime(date, "yyyy-mm-dd");
    this.NgayDen =this.heplerService.converDateTime(dateNow, "yyyy-mm-dd");
    
    this.load();
  }

  load() {
    this.orderService.getpaging(this.Limit, this.Page, this.status, this.NgayTu, this.NgayDen, this.sort, this.userID, this.Keyword)
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
    if(status < this.itemSelected.Status) {
      this.toastr.error('Không thể quay lại trạng thái trước')
      return false;
    }
    this.orderService.updateStatus(this.itemSelected.Id, status)
      .subscribe(
        (res: any) => {
          this.itemSelected.Status = status;
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
    this.orderService.delete(this.itemSelected.ProductId)
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
    this.status = this.status == "" ? null : this.status;
    this.load();
  }
}
