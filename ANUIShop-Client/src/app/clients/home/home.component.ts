import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { CategoryService } from '../../services/category.service';
import { ProductService } from '../../services/product.service';
import { StaticVaribale } from '../../_helpers/static-variable';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  modalRefCart: BsModalRef;
  dataSource: any[] = [];
  top: number = 4;
  keyword: string = "";
  sort: string = "";
  priceFrom: any = null;
  priceTo: any = null;
  url: any = StaticVaribale;
  listCategory: any[] = [];

  constructor(
    private modalService: BsModalService,
    private productService: ProductService,
    private categoryService: CategoryService
  ) { }

  ngOnInit(): void {
    this.load();
    this.getListCategory();
    this.loadSachBanChay();
    this.loadSachTN();
  }

  load() {
    this.productService.getList(this.top, "DATE_DESC", this.keyword, this.priceFrom, this.priceTo)
      .subscribe(
        (res: any) => {
          console.log(res);
          this.dataSource = res;
        },
        err => {
          console.error(err);
        }
      )
  }

  dataSourceBC: any = [];
  loadSachBanChay() {
    this.productService.getpaging2(1, 4, 0, "", "BAN_CHAY", "", "")
      .subscribe(
        (res: any) => {
          this.dataSourceBC = res.items;
        },
        err => {
          console.error(err);
        }
      )
  }

  
  dataSourceTN: any = [];
  loadSachTN() {
    this.productService.getbyparentcategory(1, 4, 7)
      .subscribe(
        (res: any) => {
          this.dataSourceTN = res.items;
        },
        err => {
          console.error(err);
        }
      )
  }

  listProductInCart: any[] = [];
  totalMoney: number = 0;
  openModalCart(template: TemplateRef<any>, item) {
    debugger;
    this.listProductInCart = JSON.parse(localStorage.getItem('listProductInCart')) || [];
    if(this.listProductInCart.length > 0) {
      const index = this.listProductInCart.map(x => x.ProductId).indexOf(item.ProductId);
      if(index >= 0) {
        this.listProductInCart[index]["Quantity"] += 1;
        this.setCart();
      }
      else {
        item['Quantity'] = 1;
        this.listProductInCart.push(item);
        this.setCart();
      }
    }
    else {
      item['Quantity'] = 1;
      this.listProductInCart.push(item);
      this.setCart();
    }

    this.modalRefCart = this.modalService.show(template, {
      animated: true,
      backdrop: 'static',
      class: 'modal-lg'
    });
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

  getListCategory() {
    debugger
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
}
