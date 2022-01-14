import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Ngxalert } from 'ngx-dialogs';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../../services/account.service';
import { StaticVaribale } from '../../_helpers/static-variable';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.scss']
})
export class AccountComponent implements OnInit {
  listRole: any[] = [];
  modalRefDelete: BsModalRef;
  modalRefChangePass: BsModalRef;
  modalRefLock: BsModalRef;
  dataSource: any = {};
  request: any = {
    Page: 1,
    Limit: 10,
    Keyword: "",
    RoleId: 0
  };
  totalPage: number = 0;
  url: any = StaticVaribale;

  passreset: any = '';
  
  constructor(
    private accountService: AccountService,
    private modalService: BsModalService,
    private toastr: ToastrService,
  ) { }

  ngOnInit(): void {
    this.load();
    this.getRole();
  }

  load() {
    this.accountService.getpaging(this.request)
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

  itemSelected: any;
  openModalDelete(template: TemplateRef<any>, item) {
    this.itemSelected = item != null ? item : {};
    this.modalRefDelete = this.modalService.show(template, {
      animated: true,
      backdrop: 'static'
    });
  }

  openModalChangePass(template: TemplateRef<any>, item) {
    this.itemSelected = item != null ? item : {};
    this.modalRefChangePass = this.modalService.show(template, {
      animated: true,
      backdrop: 'static'
    });
  }

  openModalLock(template: TemplateRef<any>, item) {
    this.itemSelected = item != null ? item : {};
    this.modalRefLock = this.modalService.show(template, {
      animated: true,
      backdrop: 'static'
    });
  }

  submitted: boolean = false;
  changePass() {
    this.submitted = true;
    if(this.passreset == null || this.passreset == '' || this.passreset.length < 6) {
      return false;
    }
    this.accountService.changePass(this.itemSelected.Id, this.passreset)
      .subscribe(
        (res: any) => {
          this.toastr.success(res);
          this.modalRefChangePass.hide();
          this.load();
        },
        err => {
          this.toastr.error(err);
        }
      )
  }

  lockAccount() {
    this.accountService.lock(this.itemSelected.Id)
    .subscribe(
      (res: any) => {
        this.toastr.success(res);
        this.modalRefLock.hide();
        this.load();
      },
      err => {
        this.toastr.error(err);
      }
    )
  }

  deleteItem() {
    this.accountService.delete(this.itemSelected.productId)
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

  getRole() {
    this.accountService.getRole()
      .subscribe(
        (res: any) => {
          this.listRole = res;
        },
        err => {
          console.error(err);
        }
      )
  }

}
