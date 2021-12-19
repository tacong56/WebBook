import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { StaticVaribale } from '../_helpers/static-variable';

@Injectable({
    providedIn: 'root'
})

export class ImageService  {
    constructor(private http: HttpClient){}

    /**
     * Hàm tải ảnh lên
     * @param value anh tu FormData
     * @returns 
     */
    upload(value: any) : Observable<any> {
      const fd = new FormData();
      fd.append("image", value);

      return this.http.post(StaticVaribale.URL + StaticVaribale.PATH.image.upload, fd);
    }

        /**
     * Hàm tải ảnh lên
     * @param value anh tu FormData
     * @returns 
     */
         uploadImageProduct(value: any, isMain: any) : Observable<any> {
          const fd = new FormData();
          fd.append("image", value);
          fd.append("isMain", isMain);
    
          return this.http.post(StaticVaribale.URL + StaticVaribale.PATH.image.uploadImageProduct, fd);
        }

  /**
   * Hàm tải ảnh lên
   * @param arr anh tu FormData
   * @returns 
   */
    uploads(arr: any) : Observable<any> {
      const fd = new FormData();
      fd.append("Images", arr);
      const body = JSON.stringify(arr);

      return this.http.post(StaticVaribale.URL + StaticVaribale.PATH.image.uploads, body, StaticVaribale.httpOptions);
    }

    /**
     * Hàm xóa ảnh
     * @param data mảng id của ảnh
     * @returns 
     */
    delete(data: any): Observable<any> {
      const body = JSON.stringify(data);
      return this.http.post(StaticVaribale.URL + StaticVaribale.PATH.image.delete, body, StaticVaribale.httpOptions);
    }
}