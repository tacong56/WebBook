import { DatePipe } from '@angular/common';
import {Component, OnInit, TemplateRef} from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { BaseComponent } from '../../components/base/base.component';
import { AccountService } from '../../services/account.service';
import { AuthService } from '../../services/auth.service';
import { CategoryService } from '../../services/category.service';
import { ImageService } from '../../services/image.service';
import { OrderService } from '../../services/order.service';
import { TokenStorageService } from '../../services/token-storage.service';
import { StaticVaribale } from '../../_helpers/static-variable';

@Component({
  selector: 'app-dashboard',
  templateUrl: './default-layout-client.component.html'
})
export class DefaultLayoutClientComponent  extends BaseComponent implements OnInit {
  modalRef: BsModalRef;
  modalRef1: BsModalRef;
  modalRefCart: BsModalRef;
  modalRefOrder: BsModalRef;
  submitted: boolean = false;
  user: any = {};
  isLogin: boolean = false;
  listCategory: any[] = [];
  url: any = StaticVaribale;

  orderStatus: any[] = [
    {id: 0, name: "Chờ xác nhận", color: "warning"},
    {id: 1, name: "Xác nhận", color: "primary"},
    {id: 2, name: "Đang giao", color: "primary"},
    {id: 3, name: "Hoàn thành", color: "success"},
    {id: 4, name: "Chờ hủy", color: "warning"},
    {id: 5, name: "Hủy", color: "danger"},
  ]

  item1: any = {
    username: "",
    password: ""
  };

  constructor(
    public fb: FormBuilder,
    private modalService: BsModalService,
    private authServie: AuthService,
    private categoryService: CategoryService,
    private authService: AuthService,
    private toastr: ToastrService,
    private tokenService: TokenStorageService,
    private router: Router,
    private route: ActivatedRoute,
    private orderService: OrderService,
    private accountService: AccountService,
    private imageService: ImageService,
    private datePipe: DatePipe
  ) 
  {
    super(fb);
  }

  ngOnInit(): void {
    this.checkLogin();
    this.getListCategory();
  }

  redirectFilterKeyword(e) {
    window.location.href = '/tim-kiem-sach?keyword='+e.currentTarget.value;
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
    this.load();
    this.setForm();
    // this.getRole();
    this.modalRef = this.modalService.show(template, {
      animated: true,
      backdrop: 'static',
      class: 'modal-lg'
    });
  }
  openModal1(template: TemplateRef<any>) {
    this.submitted = false;
    this.modalRef1 = this.modalService.show(template, {
      animated: true,
      backdrop: 'static'
    });
  }
  listProductInCart: any[] = [];
  totalMoney: number = 0;
  openModalCart(template: TemplateRef<any>, item) {
    debugger;
    this.listProductInCart = JSON.parse(localStorage.getItem('listProductInCart')) || [];
    let temp = 0;
    this.listProductInCart.map((el, index) => {
      temp += (el.Price * el.Quantity)
      return temp;
    })
    this.totalMoney = temp;

    this.modalRefCart = this.modalService.show(template, {
      animated: true,
      backdrop: 'static',
      class: 'modal-lg'
    });
  }

  openModalOrder(template: TemplateRef<any>, item) {
    this.getOrderByUser();
    this.modalRefOrder = this.modalService.show(template, {
      animated: true,
      backdrop: 'static',
      class: 'modal-lg'
    });
  }

  listOrder: any[] = [];
  getOrderByUser() {
    this.orderService.getlist(this.user.Id)
      .subscribe(
        (res: any) => {
          this.listOrder = res;
          console.log(res);
        }
      )
  }

  goToHome() {
    this.router.navigateByUrl('/');
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
            // this.checkLogin();
            window.location.reload();
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

  deleteItem(item) {
    const index = this.listProductInCart.map(x => x.ProductId).indexOf(item.ProductId);
    if(index >= 0) this.listProductInCart.splice(index, 1);
    this.setCart();
  }

  changeQuantity(e) {
    console.log(e.currentTarget.value)
    this.setCart();
  }

  clickChangeQuantity(item, type) {
    if(type=='PLUS') item.Quantity += 1;
    else if (type=='MINUS') item.Quantity -= 1;
    this.setCart();
  }

  setCart() {
    let temp = 0;
    this.listProductInCart.map((el, index) => {
      temp += (el.Price * el.Quantity)
      return temp;
    })
    this.totalMoney = temp;
    console.log(this.totalMoney)
    localStorage.setItem('listProductInCart', JSON.stringify(this.listProductInCart));
  }
  
  saveChangeSatatus(item) {
    this.orderService.updateStatus(item.Id, 4)
      .subscribe(
        (res: any) => {
          this.toastr.success("Thay đổi trạng thái thành công");
          this.getOrderByUser();
          console.log(res);
        },
        err => {
          console.error(err);
        }
      )
  }



  data: any;
  // listRole: any[] = [];
  // listUploadImage: any[] = [];
  imageMain: any = null;
  imageMainUrl: any = '';
  imageMainUrlOld: string = '/assets/img/avatars/product-default.jpg';

  setForm() {
    this.formDetail = this.fb.group({
      Id: [0, [
        Validators.required
      ]],
      UserName: ['', [
        Validators.required,
        Validators.maxLength(50)
      ]],
      FirstName: ['', [
        Validators.required,
        Validators.maxLength(250)
      ]],
      LastName: ['', [
        Validators.required,
        Validators.maxLength(250)
      ]],
      Email: ['', [
      ]],
      RoleId: [3, [
        Validators.required
      ]],
      Password: ['', [
        Validators.required,
      ]],
      Password_Repeat: ['', [
        Validators.required,
      ]],
      PhoneNumber: ['', [
      ]],
      Dob: [null, [
      ]],
      ImageID: [0, [
      ]],
    })
  }
  
  load() {
    const id = this.route.snapshot.paramMap.get('id') || 0;
    this.accountService.get(id)
      .subscribe(
        (res: any) => {
  
          var d = this.formatDateByCurrentTimeZone(res.Data.Dob);
          var url_img = res.Data.UrlImage != null ? StaticVaribale.URL_IMAGE + res.Data.UrlImage : "";
  
          this.formDetail.get('Id')?.patchValue(res.Data.Id);
          this.formDetail.get('UserName')?.patchValue(res.Data.UserName);
          this.formDetail.get('Email')?.patchValue(res.Data.Email);
          this.formDetail.get('FirstName')?.patchValue(res.Data.FirstName);
          this.formDetail.get('LastName')?.patchValue(res.Data.LastName);
          this.formDetail.get('PhoneNumber')?.patchValue(res.Data.PhoneNumber);
          this.formDetail.get('Address')?.patchValue(res.Data.Address);
          this.formDetail.get('Avatar')?.patchValue(url_img);
          this.formDetail.get('dob')?.patchValue(this.datePipe.transform(d, 'yyyy-MM-dd'));
        },
        err => {
          console.error(err);
        }
      )
  }
  
  formatDateByCurrentTimeZone(date: string | null) {
    return this.datePipe.transform(new Date(Date.parse(date + '+0000')), 'yyyy-MM-dd HH:mm:ss');
  }
  
  // getRole() {
  //   this.accountService.getRole()
  //     .subscribe(
  //       (res: any) => {
  //         this.listRole = res.filter(x => x.Id != 1);
  //       },
  //       err => {
  //         console.error(err);
  //       }
  //     )
  // }
   
  requiredImage: boolean = false;
  customSave() {
    if(this.formDetail.get('Password').value != this.formDetail.get('Password_Repeat').value) {
      this.toastr.error('Mật khẩu không trùng khớp.');
      return false;
    }
    if (this.formDetail.valid) {
      if (this.imageMain != null) {
        this.imageService.upload(this.imageMain)
          .subscribe(
            (res: any) => { 
              this.afterUploadFile(res);
            },
            err => { 
              console.error('Tải ảnh không thành công!') 
            }
          )
      }
      else {
        this.afterUploadFile(null);
      }
    }
    else {
      const properties = Object.keys(this.formDetail.controls);
      for(let i = 0; i < properties.length; i++) {
        this.validateAllFormFields(this.formDetail);
      }
    }
  }
  
  afterUploadFile(imageId) {
    if(this.formDetail.get('Id').value == 0) {
      this.formDetail.get('ImageID')?.patchValue(imageId);
      this.formDetail.get('RoleId')?.patchValue(parseInt(this.formDetail.get('RoleId').value));
      this.accountService.create(this.formDetail.value)
      .subscribe(
        (res: any) => {
          if(res.Error == 0) {
            this.toastr.success('Thêm mới thành công.');
            this.modalRef.hide();
            this.resetForm();
          }
          else if(res.Error == 1) {
            this.toastr.success(res.Msg);
          }
        },
        err => {
          console.error(err);
        }
      )
    }
    else {
      this.accountService.update(this.formDetail.value)
      .subscribe(
        (res: any) => {
          if(res.Error == 0) {
            this.toastr.success('Cập nhật thành công.');
          }
        },
        err => {
          console.error(err);
        }
      )
    }
  }
  
  resetForm() {
    this.load();
  }
  
  onSelectFileMain(event) {
    const file = event.target.files;
    if (file && file[0]) {
      var reader = new FileReader();
      reader.readAsDataURL(file[0]); // read file as data url
      reader.onload = (event) => { // called once readAsDataURL is completed
        this.imageMainUrl = event.target.result;
      }
      this.imageMain = file[0];
      this.requiredImage = false;
    }
    else this.requiredImage = true;
  }
  
  deleteImageMain() {
    this.imageMain = null;
    this.imageMainUrl = this.imageMainUrlOld;
  }
}
