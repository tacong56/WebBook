import { DatePipe } from '@angular/common';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { PromotionService } from '../../services/promotion.service';
import { UtiltiesService } from '../../services/utilties.service';

@Component({
  selector: 'app-promotion',
  templateUrl: './promotion.component.html',
  styleUrls: ['./promotion.component.scss']
})
export class PromotionComponent implements OnInit {

  modalRefDelete: BsModalRef;
  dataSource: any = {};
  request: any = {
    Page: 1,
    Limit: 10,
    Keyword: "",
    NgayTu: null,
    NgayDen: null
  };
  totalPage: number = 0;

  constructor(
    private promotionService: PromotionService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private datePipe: DatePipe,
    private heplerService: UtiltiesService
  ) { }

  ngOnInit(): void {
    const dateNow = new Date();
    const date = new Date();
    date.setFullYear(date.getFullYear() - 1);
    this.request.NgayTu = this.heplerService.converDateTime(date, "yyyy-mm-dd");
    this.request.NgayDen =this.heplerService.converDateTime(dateNow, "yyyy-mm-dd");
    console.log(this.request.NgayTu)
  }

  load() {
    this.promotionService.getpaging(this.request)
      .subscribe(
        (res: any) => {
          console.log(res);
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
    if(page != this.request.Page) {
      this.request.Page = page;
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

  deleteItem() {
    this.promotionService.delete(this.itemSelected.productId)
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
