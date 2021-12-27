import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../../services/auth.service';
import { ImageService } from '../../services/image.service';
import { StaticVaribale } from '../../_helpers/static-variable';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {
  formatDateByCurrentTimeZone(date: string | null) {
    return this.datePipe.transform(new Date(Date.parse(date + '+0000')), 'yyyy-MM-dd HH:mm:ss');
  }

  formInfo = this.fb.group({
    id: [''],
    username: [''],
    email: [''],
    firstname: [''],
    lastname: [''],
    phonenumber: [''],
    address: [''],
    urlimage: [''],
    dob: [''],
    file: [null],
  })

  constructor(private authService: AuthService, private fb: FormBuilder, 
    private datePipe: DatePipe, private toastr: ToastrService,
    private router: Router, private imageService: ImageService) { 
      this.authService.info()
      .subscribe(
        (res: any) => {
          var d = this.formatDateByCurrentTimeZone(res.Data.Dob);
          var url_img = res.Data.UrlImage != null ? StaticVaribale.URL_IMAGE + res.Data.UrlImage : "";

          this.formInfo.get('id')?.patchValue(res.Data.Id);
          this.formInfo.get('username')?.patchValue(res.Data.UserName);
          this.formInfo.get('email')?.patchValue(res.Data.Email);
          this.formInfo.get('firstname')?.patchValue(res.Data.FirstName);
          this.formInfo.get('lastname')?.patchValue(res.Data.LastName);
          this.formInfo.get('phonenumber')?.patchValue(res.Data.PhoneNumber);
          this.formInfo.get('address')?.patchValue(res.Data.Address);
          this.formInfo.get('urlimage')?.patchValue(url_img);
          this.formInfo.get('dob')?.patchValue(this.datePipe.transform(d, 'yyyy-MM-dd'));
        },
        err => {
        }
      );
    }

  ngOnInit(): void {

  }

  changeFile(event: any) {
    console.log(event);
    this.formInfo?.patchValue({
      file: event
    });
  }

  changeInfo() {
    if (this.formInfo.get('file').value != null) {
      this.imageService.upload(this.formInfo.get('file').value)
        .subscribe(
          (res: any) => { 
            this.updateInfo(res);
          },
          err => { 
            console.error('Tải ảnh không thành công!') 
          }
        )
    }
    else {
      this.updateInfo(null);
    }
  }

  updateInfo(imageId: any) {
    this.formInfo.value['imageId'] = imageId;
    this.authService.update(this.formInfo.value)
      .subscribe(
        (res: any) => {
          this.toastr.success(res.Msg);
          window.location.reload();
        },
        err => {
          let msg = '';
          if(err.error.Errors != undefined && err.error.Errors != null) {
            const key = Object.keys(err.error.Errors)[0];
            msg = err.error.Errors[key][0];
          }
          else if(err.error.Msg != undefined && err.error.Msg != null) 
            msg = err.error.Msg;
          this.toastr.error(msg)
        }
      )
  }
}
