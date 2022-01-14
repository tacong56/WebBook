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
    this.isSubmited = true;
    if(this.isSubmited && (this.itemSelected.Name == null || this.itemSelected.Name == '')) {
      this.toastr.error('Tên danh mục không được để trống.')
      return false;
    }    
    if(this.isSubmited && (this.itemSelected.SortOrder == null || this.itemSelected.SortOrder == '')) {
      this.toastr.error('Vị trí không được để trống.')
      return false;
    }
    this.itemSelected.ParentId = parseInt(this.itemSelected.ParentId);
    if(this.itemSelected.ParentId == 0) this.itemSelected.Level = 0;
    else this.itemSelected.Level = 1;
    if(this.itemSelected.Id > 0) {
      this.categoryService.update(this.itemSelected)
      .subscribe(
        (res: any) => {
          this.toastr.success('Cập nhật thành công.');
          this.modalRef.hide();
          this.reset();
        },
        err => {
          this.toastr.error(err.error.Msg);
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
          this.toastr.error(err.error.Msg);
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
    this.categoryService.delete(this.itemSelected.Id)
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
