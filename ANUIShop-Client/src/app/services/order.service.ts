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
    return this.http.get(StaticVaribale.URL + StaticVaribale.PATH.order.get + '/' + id, StaticVaribale.httpOptions)
  }

  getlist(userid: any) : Observable<any> {
    let params = new HttpParams();

    params = params.append('userID', userid);
    return this.http.get(StaticVaribale.URL + StaticVaribale.PATH.order.getlist, {params: params})
  }


  dheader() : Observable<any> {
    return this.http.get(StaticVaribale.URL + StaticVaribale.PATH.order.dorder, StaticVaribale.httpOptions)
  }

  dPie(ngaytu: any, ngayden: any) : Observable<any> {
    let params = new HttpParams();

    params = params.append('ngaytu', ngaytu + ' 00:00');
    params = params.append('ngayden', ngayden + ' 23:59');
    return this.http.get(StaticVaribale.URL + StaticVaribale.PATH.order.dpie, {params})
  }

  topProduct(ngaytu: any, ngayden: any) : Observable<any> {
    let params = new HttpParams();

    params = params.append('ngaytu', ngaytu + ' 00:00');
    params = params.append('ngayden', ngayden + ' 23:59');
    return this.http.get(StaticVaribale.URL + StaticVaribale.PATH.order.topproduct, {params})
  }

  getpaging(limit: any, page: any, status: any, ngaytu: any, ngayden: any, sort: any, userID: any, keyword: any) : Observable<any> {
    let params = new HttpParams();

    params = params.append('page', page);
    params = params.append('limit', limit);
    params = params.append('keyword', keyword);
    if(sort != undefined && sort != null && sort != '') params = params.append('sort', sort);
    if(userID != undefined && userID != null && userID > 0) params = params.append('userID', userID);
    if(status != undefined && status != null) params = params.append('status', status);
    if(ngaytu != undefined && ngaytu != null) params = params.append('ngaytu', ngaytu + ' 00:00');
    if(ngayden != undefined && ngayden != null) params = params.append('ngayden', ngayden + ' 23:59');

    return this.http.get(StaticVaribale.URL + StaticVaribale.PATH.order.getpaging, {params});
  }

  getpagingtran(limit: any, page: any, ngaytu: any, ngayden: any, sort: any) : Observable<any> {
    let params = new HttpParams();

    params = params.append('page', page);
    params = params.append('limit', limit);
    if(sort != undefined && sort != null && sort != '') params = params.append('sort', sort);
    if(ngaytu != undefined && ngaytu != null) params = params.append('ngaytu', ngaytu + ' 00:00');
    if(ngayden != undefined && ngayden != null) params = params.append('ngayden', ngayden + ' 23:59');

    return this.http.get(StaticVaribale.URL + StaticVaribale.PATH.order.getpagingtran, {params});
  }

  create(data: any) : Observable<any> {
    let dataTemp = Object.assign({}, data);
    const body = JSON.stringify(dataTemp);
    return this.http.post(StaticVaribale.URL + StaticVaribale.PATH.order.create, body, StaticVaribale.httpOptions);
  }

  createVNPay(orderID: any, domainName: any) : Observable<any> {
    return this.http.get(StaticVaribale.URL + StaticVaribale.PATH.order.createVNPay + '?orderID=' + orderID + '&domainName=' + domainName, StaticVaribale.httpOptions);
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
    return this.http.put(StaticVaribale.URL + StaticVaribale.PATH.order.updateStatus + '/' + id + '/' + status, {}, StaticVaribale.httpOptions);
  }
}
