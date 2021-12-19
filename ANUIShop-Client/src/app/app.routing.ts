import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

// Import Containers
import { DefaultLayoutComponent } from './containers';
import { ProfileComponent } from './pages/profile/profile.component';

import { P404Component } from './views/error/404.component';
import { P500Component } from './views/error/500.component';
import { LoginComponent } from './views/login/login.component';
import { RegisterComponent } from './views/register/register.component';
import { AuthGuard } from './_helpers/auth.guard';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'admin/dashboard',
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
        path: 'base',
        loadChildren: () => import('./views/base/base.module').then(m => m.BaseModule)
      },
      {
        path: 'buttons',
        loadChildren: () => import('./views/buttons/buttons.module').then(m => m.ButtonsModule)
      },
      {
        path: 'charts',
        loadChildren: () => import('./views/chartjs/chartjs.module').then(m => m.ChartJSModule)
      },
      {
        path: 'dashboard',
        loadChildren: () => import('./views/dashboard/dashboard.module').then(m => m.DashboardModule)
      },
      {
        path: 'icons',
        loadChildren: () => import('./views/icons/icons.module').then(m => m.IconsModule)
      },
      {
        path: 'notifications',
        loadChildren: () => import('./views/notifications/notifications.module').then(m => m.NotificationsModule)
      },
      {
        path: 'theme',
        loadChildren: () => import('./views/theme/theme.module').then(m => m.ThemeModule)
      },
      {
        path: 'widgets',
        loadChildren: () => import('./views/widgets/widgets.module').then(m => m.WidgetsModule),
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
    ]
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
