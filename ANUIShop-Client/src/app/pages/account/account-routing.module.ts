import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AccountDetailComponent } from './account-detail/account-detail.component';
import { AccountComponent } from './account.component';

const routes: Routes = [
  {
    path: "",
    data: {
      title: "Tài khoản"
    },
    children: [
      {
        path: "",
        component: AccountComponent,
        data: {
          title: "Danh sách"
        }
      },
      {
        path: "them-moi",
        component: AccountDetailComponent,
        data: {
          title: "Thêm mới",
        }
      },
      {
        path: "cap-nhat/:id",
        component: AccountDetailComponent,
        data: {
          title: "Cập nhật",
        }
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AccountRoutingModule { }
