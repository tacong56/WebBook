import { HttpClient, HttpEventType, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { StaticVaribale } from '../_helpers/static-variable';

@Injectable({
  providedIn: 'root'
})
export class ImportStorageService {

  constructor(private http: HttpClient) { }

  getpaging(limit: any, page: any, ngaytu: any, ngayden: any, sort: any) : Observable<any> {
    let params = new HttpParams();

    params = params.append('page', page);
    params = params.append('limit', limit);
    if(sort != undefined && sort != null && sort != '') params = params.append('sort', sort);
    if(ngaytu != undefined && ngaytu != null) params = params.append('ngaytu', ngaytu + ' 00:00');
    if(ngayden != undefined && ngayden != null) params = params.append('ngayden', ngayden + ' 23:59');

    return this.http.get(StaticVaribale.URL + StaticVaribale.PATH.importStorage.getpaging, {params});
  }

  get(id: any) : Observable<any> {
    return this.http.get(StaticVaribale.URL + StaticVaribale.PATH.importStorage.get + '/' + id)
  }

  create(data: any) : Observable<any> {
    let dataTemp = Object.assign({}, data);
    dataTemp['Price'] = typeof dataTemp['Price'] == 'number' ? dataTemp['Price'] : parseInt(dataTemp['Price'].replace(/,/g, ""));
    dataTemp['ProductId'] = parseInt(dataTemp['ProductId']);
    const body = JSON.stringify(dataTemp);
    return this.http.post(StaticVaribale.URL + StaticVaribale.PATH.importStorage.create, body, StaticVaribale.httpOptions);
  }

  update(data: any) : Observable<any> {
    let dataTemp = Object.assign({}, data);
    dataTemp['Price'] = typeof dataTemp['Price'] == 'number' ? dataTemp['Price'] : parseInt(dataTemp['Price'].replace(/,/g, ""));
    dataTemp['ProductId'] = parseInt(dataTemp['ProductId']);
    const body = JSON.stringify(dataTemp);
    return this.http.put(StaticVaribale.URL + StaticVaribale.PATH.importStorage.update, body, StaticVaribale.httpOptions);
  }
}
