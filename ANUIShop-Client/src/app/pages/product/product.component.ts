import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Ngxalert } from 'ngx-dialogs';
import { ToastrService } from 'ngx-toastr';
import { CategoryService } from '../../services/category.service';
import { ProductService } from '../../services/product.service';
import { StaticVaribale } from '../../_helpers/static-variable';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
})
export class ProductComponent implements OnInit {
  modalRefDelete: BsModalRef;
  listCategory: any[] = [];
  dataSource: any = {};
  request: any = {
    Page: 1,
    Limit: 1,
    Keyword: "",
    CategoryId: 0
  };
  totalPage: number = 0;
  url: any = StaticVaribale;

  constructor(
    private modalService: BsModalService,
    private productService: ProductService,
    private categoryService : CategoryService,
    private toastr: ToastrService,
  ) { }

  ngOnInit(): void {
    this.getListCategory();
    this.load();
  }

  load() {
    this.productService.getpaging(this.request)
      .subscribe(
        (res: any) => {
          console.log(res);
          this.dataSource = res;
          this.totalPage = Math.ceil(Math.ceil(this.dataSource.TotalRecord / this.dataSource.Limit));
          this.setListPaging();
        },
        err => {
          console.error(err);
        }
      )
  }

  getListCategory() {
    this.categoryService.getList()
      .subscribe(
        (res: any) => {
          this.listCategory = res;
          console.log(res);
        },
        err => {
          console.error(err);
        }
      )
  }

  filter() {
    this.load();
  }

  confirmAlert: any = new Ngxalert;
  deleteItem() {
    this.productService.delete(this.itemSelected.ProductId)
    .subscribe(
      (res: any) => {
        if(res == 1) this.toastr.success("Xóa sách thành công.");
        else this.toastr.error("Xóa sách không thành công.");
        this.modalRefDelete.hide();
        this.load();
      },
      err => {
        console.error(err)
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

}
