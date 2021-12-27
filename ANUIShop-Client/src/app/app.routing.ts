import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './clients/home/home.component';

// Import Containers
import { DefaultLayoutComponent } from './containers';
import { DefaultLayoutClientComponent } from './containers/default-layout-client/default-layout-client.component';
import { ProfileComponent } from './pages/profile/profile.component';

import { P404Component } from './views/error/404.component';
import { P500Component } from './views/error/500.component';
import { LoginComponent } from './views/login/login.component';
import { RegisterComponent } from './views/register/register.component';
import { AuthGuard } from './_helpers/auth.guard';

export const routes: Routes = [
  {
    path: '',
    redirectTo: '/trang-chu',
    pathMatch: 'full',
  },
  {
    path: '404',
    component: P404Component,
    data: {
      title: 'Lỗi 404'
    }
  },
  {
    path: '500',
    component: P500Component,
    data: {
      title: 'Lỗi 500'
    }
  },
  {
    path: 'admin/login',
    component: LoginComponent,
    data: {
      title: 'Đăng nhập'
    }
  },
  {
    path: 'admin/register',
    component: RegisterComponent,
    data: {
      title: 'Đăng ký'
    }
  },
  {
    path: 'admin',
    component: DefaultLayoutComponent,
    canActivate: [AuthGuard], 
    data: {
      permittedRoles: ['Admin'], title: "Trang chủ"
    },
    children: [
      {
        path: 'profile',
        component: ProfileComponent,
        data: { title: 'Thông tin cá nhân' }
      },
      {
        path: 'dashboard',
        loadChildren: () => import('./views/dashboard/dashboard.module').then(m => m.DashboardModule)
      },
      {
        path: 'san-pham',
        loadChildren: () => import('./pages/product/product.module').then(m => m.ProductModule),
      },
      {
        path: 'danh-muc',
        loadChildren: () => import('./pages/category/category.module').then(m => m.CategoryModule),
      },
      {
        path: 'tai-khoan',
        loadChildren: () => import('./pages/account/account.module').then(m => m.AccountModule),
      },
      {
        path: 'don-hang',
        loadChildren: () => import('./pages/order/order.module').then(m => m.OrderModule),
      },
      {
        path: 'khuyen-mai',
        loadChildren: () => import('./pages/promotion/promotion.module').then(m => m.PromotionModule),
      },
    ]
  },
  {
    path: "",
    component: DefaultLayoutClientComponent,
    children: [
      {
        path: '',
        loadChildren: () => import('./clients/clients.module').then(m => m.ClientsModule),
      }
    ]
    // loadChildren: () => import('./clients/clients.module').then(m => m.ClientsModule)
  },
  { path: '**', component: P404Component }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy', anchorScrolling: 'enabled' })
  ],
  exports: [ 
    RouterModule,
    CommonModule
  ]
})
export class AppRoutingModule {}
