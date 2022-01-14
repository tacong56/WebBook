import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../../services/auth.service';
import { OrderService } from '../../services/order.service';
import { TokenStorageService } from '../../services/token-storage.service';
import { StaticVaribale } from '../../_helpers/static-variable';

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.scss']
})
export class CheckoutComponent implements OnInit {

  modalRefDH: BsModalRef;
  url: any = StaticVaribale;
  listProductInCart: any[] = [];
  totalMoney: number = 0;
  user: any = {};
  isLogin: boolean = false;
  item: any = {};

  item1: any = {
    username: "",
    password: ""
  };

  constructor(
    private orderService: OrderService,
    private tokenService: TokenStorageService,
    private toastr: ToastrService,
    private modalService: BsModalService,
    private fb: FormBuilder,
    private router: Router,
    private authService: AuthService,
  ) { }

  ngOnInit(): void {
    this.checkLogin();
    this.getCart();
  }

  getCart() {
    let temp = 0;
    this.listProductInCart = JSON.parse(localStorage.getItem('listProductInCart')) || [];

    this.listProductInCart.map((el, index) => {
      temp += (el.Price * el.Quantity)
      return temp;
    })
    this.totalMoney = temp;
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

  onClickCheckout(isOnline) {
    debugger;
    let obj = {};

    if(this.isLogin) {
      obj['UserID'] = this.user.Id;
      // this.item['ShipAddress'] = this.user.Address;
      // this.item['ShipName'] = this.user.LastName + ' ' + this.user.FirstName;
      // this.item['ShipEmail'] =  this.user.Email;
      // this.item['ShipPhoneNumber'] = this.item.ShipPhoneNumber;
    }

    if(Object.keys(this.item).length <= 0 || this.item.ShipAddress == '' || this.item.ShipName == '' || this.item.ShipPhoneNumber == ''){
      this.submitted = true;
      return false;
    }

    obj['ShipAddress'] = this.item.ShipAddress;
    obj['ShipName'] = this.item.ShipName
    obj['ShipEmail'] = this.item.ShipEmail;
    obj['ShipPhoneNumber'] = this.item.ShipPhoneNumber;
    obj['Status'] = 0;
    obj['OrderDetails'] = this.listProductInCart;

    this.orderService.create(obj)
        .subscribe(
          (res: any) => {
            debugger;
            console.log(res);
            this.toastr.success('Đặt hàng thành công');
            if(isOnline) {
              this.vnpay(res.Data.Id);
            }
            else {
              this.modalRefDH.hide();
              window.location.href = `http://localhost:4200/notify-payment?orderID=${res.Data.Id}&transactionID=&msg=Đặt hàng thành công`
              localStorage.removeItem('listProductInCart');
              localStorage.setItem('listProductInCart', JSON.stringify([]));
            }
            this.submitted = false;
          },
          err => {
            console.error(err)
          }
        )
  }

  vnpay(id) {
    this.orderService.createVNPay(id, 'https://localhost:5001/')
      .subscribe(
        (res: any) => {
          console.log(res);
          this.modalRefDH.hide();
          window.open(res, "_blank");
          localStorage.removeItem('listProductInCart');
          localStorage.setItem('listProductInCart', JSON.stringify([]));
        },
        err => {
          console.error(err);
        }
      )
  }
  
  checkLogin() {
    this.user = this.tokenService.getUserClient();
    if(Object.keys(this.user).length > 0) {
      this.item['ShipAddress'] = this.user.Address;
      this.item['ShipName'] = this.user.LastName + ' ' + this.user.FirstName;
      this.item['ShipEmail'] =  this.user.Email;
      this.item['ShipPhoneNumber'] = this.item.ShipPhoneNumber;
      this.isLogin = true;
    }
    else this.isLogin = false;
  }

  submitted: boolean = false;
  openModal(template: TemplateRef<any>) {
    this.submitted = false;
    this.modalRefDH = this.modalService.show(template, {
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
}
