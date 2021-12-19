import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BaseComponent } from '../../../components/base/base.component';
import { CategoryService } from '../../../services/category.service';
import { ProductService } from '../../../services/product.service';
import * as ClassicEditor from '@ckeditor/ckeditor5-build-classic';
import { ToastrService } from 'ngx-toastr';
import { StaticVaribale } from '../../../_helpers/static-variable';
import { ImageService } from '../../../services/image.service';
import { ChangeEvent } from '@ckeditor/ckeditor5-angular';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.scss']
})
export class ProductDetailComponent extends BaseComponent implements OnInit {
  public Editor = ClassicEditor;

  headerTitle: string = "";
  data: any;
  listCategory: any[] = [];
  // listUploadImage: any[] = [];
  imageMain: any = null;
  imageMainUrl: any = '';
  imageMainUrlOld: string = '/assets/img/avatars/product-default.jpg';
  dataCKEditor: string = '';

  constructor(
      public fb: FormBuilder,
      private route: ActivatedRoute,
      private router: Router,
      private productService: ProductService,
      private categoryService : CategoryService,
      private toastr: ToastrService,
      private imageService: ImageService
    ) { 
      super(fb);
    }

  ngOnInit(): void {
    this.load();
    this.setForm();
    this.getListCategory();
  }

  setForm() {
    this.formDetail = this.fb.group({
      ID: [0, [
        Validators.required
      ]],
      CategoryID: ['', [
        Validators.required
      ]],
      Name: ['', [
        Validators.required,
        Validators.maxLength(250)
      ]],
      Code: ['', [
        Validators.required,
      ]],
      Price: ['', [
        Validators.required
      ]],
      Title: ['', [
        Validators.required,
        Validators.maxLength(250)
      ]],
      Description: ['', [
        Validators.required,
        Validators.maxLength(250)
      ]],
      ImageID: [0, [
      ]],
      IsActive: [true, [
      ]],
      View: [0, [
      ]],
      TimeCreated: [null, [
      ]],
      TimeUpdated: [null, [
      ]]
    })
  }

  load() {
    const id = this.route.snapshot.paramMap.get('id') || 0;
    this.productService.get(id)
      .subscribe(
        (res: any) => {
          if(res.data.imageMain != '' && res.data.imageMain != null) {
            this.imageMainUrl = StaticVaribale.URL_IMAGE + res.data.imageMain;
            this.imageMainUrlOld = StaticVaribale.URL_IMAGE + res.data.imageMain;
          }
          this.dataCKEditor = res.data.description;

          this.formDetail.get('ID')?.patchValue(res.data.productId);
          this.formDetail.get('CategoryID')?.patchValue(res.data.categoryId);
          this.formDetail.get('Name')?.patchValue(res.data.productName);
          this.formDetail.get('Code')?.patchValue(res.data.productCode);
          this.formDetail.get('Price')?.patchValue(res.data.price);
          this.formDetail.get('Title')?.patchValue(res.data.title);
          this.formDetail.get('Description')?.patchValue(res.data.description);
          this.formDetail.get('ImageID')?.patchValue(res.data.imageId);
          this.formDetail.get('TimeCreated')?.patchValue(res.data.timeCreated);
          this.formDetail.get('TimeUpdated')?.patchValue(res.data.userUpdate);
          this.formDetail.get('View')?.patchValue(res.data.view);
        },
        err => {
          console.error(err);
        }
      )
  }

  getListCategory() {
    this.categoryService.getList()
      .subscribe(
        (res: any) => {
          this.listCategory = res;
          console.log(res);
        },
        err => {
          console.error(err);
        }
      )
  }

  requiredImage: boolean = false;
  customSave() {
    if(this.imageMain == null) {
      this.requiredImage = true;
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
    if(this.formDetail.get('ID').value == 0) {
      this.formDetail.get('ImageID')?.patchValue(imageId);
      this.productService.create(this.formDetail.value)
      .subscribe(
        (res: any) => {
          if(res.error == 0) {
            this.toastr.success('Thêm mới sản phẩm thành công.');
            this.resetForm();
          }
        },
        err => {
          console.error(err);
        }
      )
    }
    else {
      this.productService.update(this.formDetail.value)
      .subscribe(
        (res: any) => {
          if(res.error == 0) {
            this.toastr.success('Cập nhật sản phẩm thành công.');
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

  changeCkEditor({editor}: ChangeEvent) {
    const data = editor.getData();
    this.formDetail.get('Description')?.patchValue(data);
  }
}
