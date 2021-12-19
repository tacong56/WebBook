import { Component, Output, EventEmitter } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-confirm-dialog',
  templateUrl: './confirm-dialog.component.html',
  styleUrls: ['./confirm-dialog.component.scss']
})
export class ConfirmDialogComponent {
  title: string;
  message: string;
  options: string[];
  answer: string = "";

  constructor(
    public bsModalRef: BsModalRef,
  ) { }

  respond(answer: any) {
    debugger
    this.answer = answer.name;
    if(answer.isClose) 
      this.bsModalRef.hide();
  }

}
