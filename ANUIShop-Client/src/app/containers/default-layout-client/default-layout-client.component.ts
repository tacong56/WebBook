import {Component, OnInit, TemplateRef} from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../../services/auth.service';
import { CategoryService } from '../../services/category.service';
import { TokenStorageService } from '../../services/token-storage.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './default-layout-client.component.html'
})
export class DefaultLayoutClientComponent implements OnInit {
  modalRef: BsModalRef;
  modalRef1: BsModalRef;
  submitted: boolean = false;
  user: any = {};
  isLogin: boolean = false;
  listCategory: any[] = [];

  item1: any = {
    username: "",
    password: ""
  };

  constructor(
    private modalService: BsModalService,
    private authServie: AuthService,
    private categoryService: CategoryService,
    private authService: AuthService,
    private toastr: ToastrService,
    private tokenService: TokenStorageService,
    private router: Router
  ) 
  {
    
  }

  ngOnInit(): void {
    this.checkLogin();
    this.getListCategory();
  }

  getListCategory() {
    this.categoryService.getListNoAuth()
      .subscribe(
        (res: any) => {
          this.listCategory = [...this.groupArr(res)]
        },
        err => {
          console.error(err);
        }
      )
  }

  groupArr(arr) {
    let arrParent = [];
    arr.map((el, index) => {
      if(el.ParentId == 0) arrParent.push(el);
      return el;
    })
    arrParent.map((el, index) => {
      el["value"] = arr.filter(x => x.ParentId == el.Id);
      return el;
    })
    console.log(arrParent);
    return arrParent;
  }

  openModal(template: TemplateRef<any>) {
    this.submitted = false;
    this.modalRef = this.modalService.show(template, {
      animated: true,
      backdrop: 'static'
    });
  }
  openModal1(template: TemplateRef<any>) {
    this.submitted = false;
    this.modalRef1 = this.modalService.show(template, {
      animated: true,
      backdrop: 'static'
    });
  }

  login() {
    this.submitted = true;
    if(this.item1.username == "" || this.item1.password == "" || this.item1.username.length < 4 || this.item1.password.length < 4) {
      return;
    }
    else {
      this.authService.loginClient(this.item1)
        .subscribe(
          (res: any) => {
            console.log(res);
            this.tokenService.saveTokenClient(res.Data.Token);
            this.tokenService.saveUserClient(res.Data);
            this.modalRef1.hide();
            this.checkLogin();
          },
          err => {
            let msg = '';
            if(err.error.Errors != undefined && err.error.Errors != null) {
              const key = Object.keys(err.error.Errors)[0];
              msg = err.error.Errors[key][0];
            }
            else if(err.error.Msg != undefined && err.error.Msg != null) 
              msg = err.error.Msg;
            this.toastr.error(msg)
          }
        )
    }
  }

  checkLogin() {
    this.user = this.tokenService.getUserClient();
    if(Object.keys(this.user).length > 0) {
      this.isLogin = true;
    }
    else this.isLogin = false;
  }

  logout() {
    this.authService.logoutClient();
    window.location.reload();
  }
}
