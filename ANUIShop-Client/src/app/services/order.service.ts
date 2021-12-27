import { HttpClient, HttpEventType, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { StaticVaribale } from '../_helpers/static-variable';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  constructor(private http: HttpClient) { }

  // index(request: any) : Observable<any> {
  //   httpOptions.headers.append('Content-Type', 'application/json');
  //   return this.http.post( baseUrl, request, httpOptions );
  // }

  get(id: any) : Observable<any> {
    return this.http.get(StaticVaribale.URL + StaticVaribale.PATH.product.update, StaticVaribale.httpOptions)
  }

  getpaging(limit: any, page: any, sort: any, userID: any, keyword) : Observable<any> {
    let params = new HttpParams();

    params = params.append('page', page);
    params = params.append('limit', limit);
    params = params.append('keyword', keyword);
    if(sort != undefined && sort != null && sort != '') params = params.append('sort', sort);
    if(userID != undefined && userID != null && userID > 0) params = params.append('userID', userID);

    return this.http.get(StaticVaribale.URL + StaticVaribale.PATH.order.getpaging, {params});
  }

  create(data: any) : Observable<any> {
    let dataTemp = Object.assign({}, data);
    const body = JSON.stringify(dataTemp);
    return this.http.post(StaticVaribale.URL + StaticVaribale.PATH.order.create, body, StaticVaribale.httpOptions);
  }

  update(data: any) : Observable<any> {
    let dataTemp = Object.assign({}, data);
    dataTemp['Price'] = typeof dataTemp['Price'] == 'number' ? dataTemp['Price'] : parseInt(dataTemp['Price'].replace(/,/g, ""));
    dataTemp['CategoryID'] = parseInt(dataTemp['CategoryID']);
    const body = JSON.stringify(dataTemp);
    return this.http.put(StaticVaribale.URL + StaticVaribale.PATH.product.update, body, StaticVaribale.httpOptions);
  }

  delete(id: any) : Observable<any> {
    return this.http.delete(StaticVaribale.URL + StaticVaribale.PATH.product.delete + '/' + id);
  }

  getRole() {
    return this.http.get(StaticVaribale.URL + 'role/getlist');
  }

  updateStatus(id: any, status: any) {
    let params = new HttpParams();

    params = params.append('id', id);
    params = params.append('status', status);
    return this.http.put(StaticVaribale.URL + StaticVaribale.PATH.order.updateStatus, {params}, StaticVaribale.httpOptions);
  }
}
