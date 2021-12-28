import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { BaseComponent } from '../../../components/base/base.component';
import { AccountService } from '../../../services/account.service';
import { ImageService } from '../../../services/image.service';
import { StaticVaribale } from '../../../_helpers/static-variable';

@Component({
  selector: 'app-account-detail',
  templateUrl: './account-detail.component.html',
  styleUrls: ['./account-detail.component.scss']
})
export class AccountDetailComponent extends BaseComponent implements OnInit {
  headerTitle: string = "";
  data: any;
  listRole: any[] = [];
  // listUploadImage: any[] = [];
  imageMain: any = null;
  imageMainUrl: any = '';
  imageMainUrlOld: string = '/assets/img/avatars/product-default.jpg';

  constructor(
    public fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private toastr: ToastrService,
    private accountService: AccountService,
    private imageService: ImageService
  ) { 
    super(fb);
  }

ngOnInit(): void {
  this.load();
  this.setForm();
  this.getRole();
}

setForm() {
  this.formDetail = this.fb.group({
    Id: [0, [
      Validators.required
    ]],
    UserName: ['', [
      Validators.required,
      Validators.maxLength(50)
    ]],
    FirstName: ['', [
      Validators.required,
      Validators.maxLength(250)
    ]],
    LastName: ['', [
      Validators.required,
      Validators.maxLength(250)
    ]],
    Email: ['', [
    ]],
    RoleId: ['', [
      Validators.required
    ]],
    Password: ['', [
      Validators.required,
    ]],
    Password_Repeat: ['', [
      Validators.required,
    ]],
    PhoneNumber: ['', [
    ]],
    Dob: [null, [
    ]],
    ImageID: [0, [
    ]],
  })
}

load() {
  const id = this.route.snapshot.paramMap.get('id') || 0;
  this.accountService.get(id)
    .subscribe(
      (res: any) => {
        // if(res.data.imageMain != '' && res.data.imageMain != null) {
        //   this.imageMainUrl = StaticVaribale.URL_IMAGE + res.data.imageMain;
        //   this.imageMainUrlOld = StaticVaribale.URL_IMAGE + res.data.imageMain;
        // }

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
        console.error(err);
      }
    )
}

getRole() {
  this.accountService.getRole()
    .subscribe(
      (res: any) => {
        this.listRole = res;
      },
      err => {
        console.error(err);
      }
    )
}

requiredImage: boolean = false;
customSave() {
  if(this.formDetail.get('Password').value != this.formDetail.get('Password_Repeat').value) {
    this.toastr.error('Mật khẩu không trùng khớp.');
    return false;
  }
  if (this.formDetail.valid) {
    if (this.imageMain != null) {
      this.imageService.upload(this.imageMain)
        .subscribe(
          (res: any) => { 
            this.afterUploadFile(res);
          },
          err => { 
            console.error('Tải ảnh không thành công!') 
          }
        )
    }
    else {
      this.afterUploadFile(null);
    }
  }
  else {
    const properties = Object.keys(this.formDetail.controls);
    for(let i = 0; i < properties.length; i++) {
      this.validateAllFormFields(this.formDetail);
    }
  }
}

afterUploadFile(imageId) {
  if(this.formDetail.get('Id').value == 0) {
    this.formDetail.get('ImageID')?.patchValue(imageId);
    this.formDetail.get('RoleId')?.patchValue(parseInt(this.formDetail.get('RoleId').value));
    this.accountService.create(this.formDetail.value)
    .subscribe(
      (res: any) => {
        if(res.error == 0) {
          this.toastr.success('Thêm mới thành công.');
          this.resetForm();
        }
      },
      err => {
        console.error(err);
      }
    )
  }
  else {
    this.accountService.update(this.formDetail.value)
    .subscribe(
      (res: any) => {
        if(res.error == 0) {
          this.toastr.success('Cập nhật thành công.');
        }
      },
      err => {
        console.error(err);
      }
    )
  }
}

resetForm() {
  this.load();
}

onSelectFileMain(event) {
  const file = event.target.files;
  if (file && file[0]) {
    var reader = new FileReader();
    reader.readAsDataURL(file[0]); // read file as data url
    reader.onload = (event) => { // called once readAsDataURL is completed
      this.imageMainUrl = event.target.result;
    }
    this.imageMain = file[0];
    this.requiredImage = false;
  }
  else this.requiredImage = true;
}

deleteImageMain() {
  this.imageMain = null;
  this.imageMainUrl = this.imageMainUrlOld;
}

}
