import { Component, OnInit, TemplateRef } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { CategoryService } from '../../services/category.service';
import { ProductService } from '../../services/product.service';
import { StaticVaribale } from '../../_helpers/static-variable';

@Component({
  selector: 'app-page-product',
  templateUrl: './page-product.component.html',
  styleUrls: ['./page-product.component.scss']
})
export class PageProductComponent implements OnInit {

  modalRefCart: BsModalRef;

  dataSource: any = {};
  Page: any = 1;
  Limit: any = 20;
  Keyword: any = "";
  Where: any = null;
  totalPage: any = 0;
  sortprice: any = "";
  sortname: any = "";
  categoryid: any = null;
  url: any = StaticVaribale;

  constructor(
    private modalService: BsModalService,
    private productService: ProductService,
    private categoryService: CategoryService,
    private route: ActivatedRoute,
  ) { }

  ngOnInit(): void {
    this.categoryid = this.route.snapshot.paramMap.get('id') || 0;
    this.load();
  }

  load() {
    this.productService.getpaging2(this.Page, this.Limit, this.categoryid, this.Keyword, this.sortprice, this.sortname, this.Where)
      .subscribe(
        (res: any) => {
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
    if(page != this.Page) {
      this.Page = page;
      this.load();
    }
  }

  //-----cart---------------
  listProductInCart: any[] = [];
  totalMoney: number = 0;
  openModalCart(template: TemplateRef<any>, item) {
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

  changeQuantity(e, item) {
    if(e.currentTarget.value <= 1) item.Quantity = 1;
    if(e.currentTarget.value > item.LuongTon) item.Quantity = item.LuongTon;
    this.setCart();
  }

  clickChangeQuantity(item, type) {
    if(type=='PLUS' && item.Quantity < item.LuongTon) item.Quantity += 1;
    else if (type=='MINUS' && item.Quantity > 1) item.Quantity -= 1;
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
   //-----cart---------------
}
