import { HttpClient, HttpEventType, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { StaticVaribale } from '../_helpers/static-variable';

const baseUrl = 'https://localhost:5001/api/product/';
const httpOptions = {
    headers: new HttpHeaders({
      // 'Content-Type': 'application/json',
    }),
};

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private http: HttpClient) { }

  // index(request: any) : Observable<any> {
  //   httpOptions.headers.append('Content-Type', 'application/json');
  //   return this.http.post( baseUrl, request, httpOptions );
  // }

  get(id: any) : Observable<any> {
    return this.http.get(baseUrl + `get/${id}`, StaticVaribale.httpOptions)
  }

  getpaging(request: any) : Observable<any> {
    let params = new HttpParams();
    params = params.append('Page', request.Page);
    params = params.append('Limit', request.Limit);
    params = params.append('Keyword', request.Keyword);
    params = params.append('CategoryId', request.CategoryId);

    return this.http.get(StaticVaribale.URL + StaticVaribale.PATH.product.getpaging, {params});
  }

  getpagingnoauth(request: any) : Observable<any> {
    let params = new HttpParams();
    params = params.append('Page', request.Page);
    params = params.append('Limit', request.Limit);
    params = params.append('Keyword', request.Keyword);
    params = params.append('CategoryId', request.CategoryId);

    return this.http.get(StaticVaribale.URL + StaticVaribale.PATH.product.getpagingnoauth, {params});
  }

  getList(top: any, sort: any, keyword: any, priceFrom: any, priceTo: any) : Observable<any> {
    let params = new HttpParams();
    params = params.append('top', top);
    params = params.append('sort', sort);
    params = params.append('Keyword', keyword);
    if(priceFrom != null)
      params = params.append('priceFrom', priceFrom);
    if(priceTo != null)
      params = params.append('priceTo', priceTo);

    return this.http.get(StaticVaribale.URL + StaticVaribale.PATH.product.getlist, {params});
  }

  create(data: any) : Observable<any> {
    let dataTemp = Object.assign({}, data);
    dataTemp['Price'] = typeof dataTemp['Price'] == 'number' ? dataTemp['Price'] : parseInt(dataTemp['Price'].replace(/,/g, ""));
    dataTemp['CategoryID'] = parseInt(dataTemp['CategoryID']);
    const body = JSON.stringify(dataTemp);
    return this.http.post(baseUrl + `create`, body, StaticVaribale.httpOptions);
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
}
