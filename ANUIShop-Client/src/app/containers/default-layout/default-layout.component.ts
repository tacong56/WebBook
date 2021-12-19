import {Component, OnInit} from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { StaticVaribale } from '../../_helpers/static-variable';
import { navItems } from '../../_nav';

@Component({
  selector: 'app-dashboard',
  templateUrl: './default-layout.component.html'
})
export class DefaultLayoutComponent implements OnInit {
  public sidebarMinimized = false;
  public navItems = navItems;
  
  url_img = StaticVaribale.URL_IMAGE;
  avatar: string;
  user = this.authServie.info()
    .subscribe(
      (res: any) => {
        this.user = res;
        this.avatar = res.data.urlImage != null ? this.url_img + res.data.urlImage : "assets/img/avatars/avatar-default.png";
      },
      err => {
      }
    );

  constructor(private authServie: AuthService) {}

  ngOnInit(): void {
  }

  toggleMinimize(e) {
    this.sidebarMinimized = e;
  }

  logout() {
    this.authServie.logout();
  }
}
