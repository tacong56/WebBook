import { Component, ElementRef, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { ImageService } from '../../services/image.service';

@Component({
  selector: 'app-upload-image',
  templateUrl: './upload-image.component.html',
  styleUrls: ['./upload-image.component.scss']
})
export class UploadImageComponent implements OnInit {
  // @ViewChild('fileInput') fileInput: ElementRef;
  
  @Input() url;
  @Input() urlOld;

  @Output() ChangeFileEvent = new EventEmitter();

  constructor(private imageService: ImageService) { }

  ngOnInit(): void {
  }

  onSelectFile(event) {
    const file = event.target.files;
    if (file && file[0]) {
      // const request = {
      //   image: file[0]
      // };

      var reader = new FileReader();
      reader.readAsDataURL(file[0]); // read file as data url
      reader.onload = (event) => { // called once readAsDataURL is completed
        this.url = event.target.result;
      }

      this.ChangeFileEvent.emit(file[0]);
    }
  }

  public delete(){
    this.ChangeFileEvent.emit(null);
    // this.fileInput.nativeElement.value = '';
    this.url = this.urlOld;
  }
}
