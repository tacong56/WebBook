import { AfterViewInit, Component, ElementRef, HostListener, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { TokenStorageService } from '../../services/token-storage.service';
import { ToastrService } from 'ngx-toastr';
import { share } from 'rxjs/operators';

@Component({
  selector: 'app-dashboard',
  templateUrl: 'login.component.html'
})
export class LoginComponent implements OnInit, AfterViewInit {
  hide = true

  constructor(private fb: FormBuilder, private authService: AuthService,
    private tokenService: TokenStorageService, private router: Router, private toastr: ToastrService, private route: ActivatedRoute) { }
  
  ngAfterViewInit() {
  }
  ngOnInit(): void {
  }
  
  formLogin = this.fb.group({
    username: ['', [
      Validators.required, 
      Validators.minLength(4)
    ]],
    password: ['', [
      Validators.required,
      Validators.minLength(4)
    ]]
  })

  login() {
    if (this.formLogin.valid) {
      this.authService.login(this.formLogin.value)
      .subscribe(
        (res: any) => {
          if(res.error == 0) {
            this.tokenService.saveToken(res.data.token);
            this.tokenService.saveUser(res.data);
            this.router.navigateByUrl('/admin/dashboard');
          }
        },
        err => {
          let msg = '';
          if(err.error.errors != undefined && err.error.errors != null) {
            const key = Object.keys(err.error.errors)[0];
            msg = err.error.errors[key][0];
          }
          else if(err.error.msg != undefined && err.error.msg != null) 
            msg = err.error.msg;
          this.toastr.error(msg)
        }
      )
    }
    else {
      const properties = Object.keys(this.formLogin.controls);
      for(let i = 0; i < properties.length; i++) {
        this.validateAllFormFields(this.formLogin);
      }
    }
  }

  isFieldValid(field: string) {
    return (!this.formLogin.get(field).valid && this.formLogin.get(field).touched);
  }

  displayFieldCss(field: string) {
    return {
      'is-invalid': this.isFieldValid(field),
      'has-feedback': this.isFieldValid(field)
    };
  }

  validateAllFormFields(formGroup: FormGroup) {         //{1}
    Object.keys(formGroup.controls).forEach(field => {  //{2}
      const control = formGroup.get(field);             //{3}
      if (control instanceof FormControl) {             //{4}
        control.markAsTouched({ onlySelf: true });
      } else if (control instanceof FormGroup) {        //{5}
        this.validateAllFormFields(control);            //{6}
      }
    });
  }
}
