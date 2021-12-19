import { Injectable } from '@angular/core';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { Observable } from 'rxjs';
import { ConfirmDialogComponent } from '../confirm-dialog/confirm-dialog.component';


@Injectable({
  providedIn: 'root'
})

export class MessageService {
  bsModalRef: BsModalRef;

  constructor(
    private bsModalService: BsModalService,
  ) { }

  confirm(title: string, message: string, options: any[]): Observable<string> {
    debugger;
    const initialState = {
      title: title,
      message: message,
      options: options,
      answer: "",
      modal: null
    };
    this.bsModalRef = this.bsModalService.show(ConfirmDialogComponent, { initialState });

    return new Observable<string>(this.getConfirmSubscriber());
  }

  hide() {
    this.bsModalRef.hide();
  }

  private getConfirmSubscriber() {
    debugger;
    return (observer) => {
      const subscription = this.bsModalService.onHidden.subscribe((reason: string) => {
        observer.next(this.bsModalRef.content.answer);
        observer.complete();
      });

      return {
        unsubscribe() {
          subscription.unsubscribe();
        }
      };
    }
  }

}
