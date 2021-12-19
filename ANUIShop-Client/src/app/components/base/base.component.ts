import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-base',
  templateUrl: './base.component.html',
  styleUrls: ['./base.component.scss']
})
export class BaseComponent implements OnInit {

  formDetail = null;

  constructor(
    protected fb: FormBuilder
  ) { }

  ngOnInit(): void {
  }

  isFieldValid(field: string) {
    return (!this.formDetail.get(field).valid && this.formDetail.get(field).touched);
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

  onKeyOnlyNumber(e) {
    const numbers = new RegExp(/^[0-9]+$/);
    if(numbers.test(e.currentTarget.value)) {
      return;
    }
    else e.preventDefault();
  }
}
