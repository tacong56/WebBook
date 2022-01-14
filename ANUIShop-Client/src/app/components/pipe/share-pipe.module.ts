import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NumberPipePipe } from './number-pipe.pipe';



@NgModule({
  declarations: [
    NumberPipePipe
  ],
  imports: [

  ],
  exports: [NumberPipePipe]
})
export class SharePipeModule { }
