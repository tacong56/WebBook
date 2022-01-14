import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DashboardNewComponent } from './dashboard-new/dashboard-new.component';

import { DashboardComponent } from './dashboard.component';

const routes: Routes = [
  {
    path: '',
    component: DashboardNewComponent,
    data: {
      title: 'Dashboard'
    }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DashboardRoutingModule {}
