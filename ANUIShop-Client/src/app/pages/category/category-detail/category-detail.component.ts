import { AfterViewInit, Component, Input, OnInit } from '@angular/core';
import { CategoryService } from '../../../services/category.service';

@Component({
  selector: 'app-category-detail',
  templateUrl: './category-detail.component.html',
  styleUrls: ['./category-detail.component.scss']
})
export class CategoryDetailComponent implements OnInit, AfterViewInit {
  @Input() item;
  listCategory: any[] = [];

  constructor(private categoryService: CategoryService) { }

  ngOnInit(): void {
  }

  ngAfterViewInit() {
    this.getListCategory();
    if(Object.keys(this.item).length == 0) {
      this.item["isShowOnHome"] = true;
      this.item["parentId"] = 0;
      this.item["status"] = 0;
      this.item["level"] = 0;
    }
  }

  getListCategory() {
    this.categoryService.getList(0)
      .subscribe(
        (res: any) => {
          this.listCategory = res;
          console.log(res);
        },
        err => {
          console.error(err);
        }
      )
  }
}
