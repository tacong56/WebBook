import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CategoryComponent } from './category.component';

const routes: Routes = [
  {
    path: "",
    data: {
      title: "Danh mục"
    },
    children: [
      {
        path: "",
        component: CategoryComponent,
        data: {
          title: "Danh sách"
        }
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CategoryRoutingModule { }
