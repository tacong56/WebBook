import { Component, OnInit } from '@angular/core';
import { OrderService } from '../../services/order.service';
import { TokenStorageService } from '../../services/token-storage.service';
import { StaticVaribale } from '../../_helpers/static-variable';

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.scss']
})
export class CheckoutComponent implements OnInit {

  url: any = StaticVaribale;
  listProductInCart: any[] = [];
  totalMoney: number = 0;
  user: any = {};
  isLogin: boolean = false;

  constructor(
    private orderService: OrderService,
    private tokenService: TokenStorageService,
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

  onClickCheckout() {
    for(let i = 0; i < this.listProductInCart.length; i++) {
      const item = this.listProductInCart[i];
      let obj = {};
      let arr = [];
      obj['UserId'] = this.user.Id;
      obj['ShipAddress'] = this.user.Address;
      obj['ShipName'] = this.user.LastName + this.user.FirstName;
      obj['ShipEmail'] = this.user.Email;
      obj['ShipPhoneNumber'] = this.user.PhoneNumber;
      obj['Status'] = 0;
      obj['OrderDetails'] = this.listProductInCart;
      this.orderService.create(obj)
        .subscribe(
          (res: any) => {
            console.log(res);
          },
          err => {
            console.error(err)
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
}
