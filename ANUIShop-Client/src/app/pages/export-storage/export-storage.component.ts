import { Component, OnInit } from '@angular/core';
import { ExportStorageService } from '../../services/export-storage.service';
import { ProductService } from '../../services/product.service';
import { UtiltiesService } from '../../services/utilties.service';

@Component({
  selector: 'app-export-storage',
  templateUrl: './export-storage.component.html',
  styleUrls: ['./export-storage.component.scss']
})
export class ExportStorageComponent implements OnInit {

  dataSource: any = {};
  Page: any = 1;
  Limit: any = 10;
  NgayTu: any = null;
  NgayDen: any = null;
  totalPage: any = 0;
  sort: any = "";

  constructor(
    private heplerService: UtiltiesService,
    private exportStorage: ExportStorageService
  ) { }

  ngOnInit(): void {
    const dateNow = new Date();
    const date = new Date();
    date.setFullYear(date.getFullYear() - 1);
    this.NgayTu = this.heplerService.converDateTime(date, "yyyy-mm-dd");
    this.NgayDen =this.heplerService.converDateTime(dateNow, "yyyy-mm-dd");
    console.log(this.NgayTu);
    this.load();
  }

  load() {
    this.exportStorage.getpaging(this.Limit, this.Page, this.NgayTu, this.NgayDen, this.sort)
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

  filter() {
    this.load();
  }
}
