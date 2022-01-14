import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { BaseComponent } from '../../../components/base/base.component';
import { ImportStorageService } from '../../../services/import-storage.service';
import { ProductService } from '../../../services/product.service';

@Component({
  selector: 'app-import-storage-detail',
  templateUrl: './import-storage-detail.component.html',
  styleUrls: ['./import-storage-detail.component.scss']
})
export class ImportStorageDetailComponent  extends BaseComponent implements OnInit {

  filterProduct: string = "";
  headerTitle: string = "";
  constructor(
    public fb: FormBuilder,
    private toastr: ToastrService,
    private route: ActivatedRoute,
    private importStorage: ImportStorageService,
    private productService: ProductService
  ) { 
    super(fb);
  }

  ngOnInit(): void {
    this.load();
    this.getProduct();
  }

  setForm() {
    this.formDetail = this.fb.group({
      Id: [0, [
        Validators.required
      ]],
      ProductId: ['', [
        Validators.required
      ]],
      Price: ['', [
        Validators.required
      ]],
      Quantity: ['', [
        Validators.required,
      ]]
    })
  }

  load() {
    const id = this.route.snapshot.paramMap.get('id') || 0;
    this.headerTitle = id > 0 ? "Cập nhật" : "Thêm mới";
    this.setForm();
    if(id > 0) {
      this.importStorage.get(id)
      .subscribe(
        (res: any) => {
          this.formDetail.get('Id')?.patchValue(res.Data.Id);
          this.formDetail.get('ProductId')?.patchValue(res.Data.ProductId);
          this.formDetail.get('Price')?.patchValue(res.Data.Price);
          this.formDetail.get('Quantity')?.patchValue(res.Data.Quantity);
        },
        err => {
          console.error(err);
        }
      )
    }
  }

  listProduct: any[] = [];
  getProduct() {
    this.productService.getall(this.filterProduct)
      .subscribe(
        (res: any) => {
          this.listProduct = res;
        },
        err => {
          console.error(err);
        }
      )
  }

  customSave() {
    this.submitted = true;
    if (this.formDetail.valid) { 
      if(this.formDetail.get('Id').value > 0) {
        this.importStorage.update(this.formDetail.value)
          .subscribe(
            (res: any) => {
              this.toastr.success('Cập nhật thành công.');
              this.submitted = false;
              this.load();
            },
            err => {
              console.error(err);
            }
          )
      }
      else {
        this.importStorage.create(this.formDetail.value)
          .subscribe(
            (res: any) => {
              this.toastr.success('Thêm mới thành công.');
              this.submitted = false;
              this.load();
            },
            err => {
              console.error(err);
            }
          )
      }
    }
    else {
      const properties = Object.keys(this.formDetail.controls);
      for(let i = 0; i < properties.length; i++) {
        this.validateAllFormFields(this.formDetail);
      }
    }
  }

  onChangeProduct(e) {
    const productId = e.currentTarget.value;
    const product = this.listProduct.find(x => x.ProductId == productId);
    if(product != undefined) {
      if(product.Price !=  this.formDetail.get('Price').value) {
        this.formDetail.get('Price')?.patchValue(product.Price);
      }
    }
    else {
      this.formDetail.get('Price')?.patchValue('');
    }
  }

}
