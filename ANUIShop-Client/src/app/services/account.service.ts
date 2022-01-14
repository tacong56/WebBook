import { HttpClient, HttpEventType, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { StaticVaribale } from '../_helpers/static-variable';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  constructor(private http: HttpClient) { }

  // index(request: any) : Observable<any> {
  //   httpOptions.headers.append('Content-Type', 'application/json');
  //   return this.http.post( baseUrl, request, httpOptions );
  // }

  get(id: any) : Observable<any> {
    return this.http.get(StaticVaribale.URL + StaticVaribale.PATH.account.get + '/' + id, StaticVaribale.httpOptions)
  }

  getpaging(request: any) : Observable<any> {
    let params = new HttpParams();
    params = params.append('Page', request.Page);
    params = params.append('Limit', request.Limit);
    params = params.append('Keyword', request.Keyword);
    params = params.append('CategoryId', request.CategoryId);

    return this.http.get(StaticVaribale.URL + StaticVaribale.PATH.account.getpaging, {params});
  }

  create(data: any) : Observable<any> {
    let dataTemp = Object.assign({}, data);
    const body = JSON.stringify(dataTemp);
    return this.http.post(StaticVaribale.URL + StaticVaribale.PATH.account.create, body, StaticVaribale.httpOptions);
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

  changePass(id: any, pass: any) : Observable<any> {
    let data = {
      Id: id,
      Password: pass
    };

    const body = JSON.stringify(data);

    return this.http.post(StaticVaribale.URL + StaticVaribale.PATH.account.changepass, body, StaticVaribale.httpOptions);
  }

  lock(id: any) : Observable<any> {
    return this.http.post(StaticVaribale.URL + StaticVaribale.PATH.account.lock + '/' + id, null, StaticVaribale.httpOptions);
  }

  getRole() {
    return this.http.get(StaticVaribale.URL + 'role/getlist');
  }
}
