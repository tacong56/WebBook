import { Component, OnInit, Output, TemplateRef, ViewChild } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { CategoryService } from '../../services/category.service';
import { MessageService } from '../../services/message.service';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.scss']
})
export class CategoryComponent implements OnInit {
  modalRef: BsModalRef;
  modalRefDelete: BsModalRef;
  itemSelected: any = {};
  dataSource: any = {};
  request: any = {
    Page: 1,
    Limit: 10,
    Keyword: "",
    Level: ""
  };
  totalPage: number = 0;
  title: string = '';
  isSubmited: boolean = false;

  constructor(
    private modalService: BsModalService,
    private categoryService: CategoryService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.load();
  }

  
  load() {
    this.categoryService.getpaging(this.request)
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

  filter() {
    this.load();
  }

  openModal(template: TemplateRef<any>, item: any) {
    this.title = item != null ? "Cập nhật danh mục" : "Thêm mới danh mục";
    this.itemSelected = item != null ? item : {};
    this.modalRef = this.modalService.show(template, {
      animated: true,
      backdrop: 'static'
    });
  }

  customSave() {
    console.log(this.itemSelected);
    this.itemSelected.parentId = parseInt(this.itemSelected.parentId);
    if(this.itemSelected.parentId == 0) this.itemSelected.level = 0;
    else this.itemSelected.level = 1;
    if(this.itemSelected.id > 0) {
      this.categoryService.update(this.itemSelected)
      .subscribe(
        (res: any) => {
          this.toastr.success('Cập nhật thành công.');
          this.modalRef.hide();
          this.reset();
        },
        err => {
          this.toastr.error(err.error.msg);
        }
      )
    }
    else {
      this.categoryService.create(this.itemSelected)
      .subscribe(
        (res: any) => {
          this.toastr.success('Thêm mới thành công.');
          this.modalRef.hide();
          this.reset();
        },
        err => {
          this.toastr.error(err.error.msg);
        }
      )
    }

  }
  
  openModalDelete(template: TemplateRef<any>, item) {
    this.itemSelected = item != null ? item : {};
    this.modalRefDelete = this.modalService.show(template, {
      animated: true,
      backdrop: 'static'
    });
  }

  deleteItem() {
    this.categoryService.delete(this.itemSelected.id)
    .subscribe(
      (res: any) => {
        this.toastr.success('Xóa thành công.');
        this.modalRefDelete.hide();
        this.reset();
      },
      err => {
        this.toastr.error("Xóa không thành công");
      }
    )
  }

  reset() {
    this.load();
  }
}
