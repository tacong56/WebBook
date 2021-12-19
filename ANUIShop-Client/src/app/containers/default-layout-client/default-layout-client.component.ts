import {Component, OnInit} from '@angular/core';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './default-layout-client.component.html'
})
export class DefaultLayoutClientComponent implements OnInit {

  constructor(private authServie: AuthService) {}

  ngOnInit(): void {
  }
}
