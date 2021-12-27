import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { StaticVaribale } from '../_helpers/static-variable';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(private http: HttpClient) { }

  get(id: any) : Observable<any> {
    return this.http.get(StaticVaribale.URL + StaticVaribale.PATH.categogry.detail + '/' + id)
  }

  getList(level: any = null) : Observable<any> {
    let params = new HttpParams();
    if(level != null)
        params = params.append('Level', level);
    return this.http.get(StaticVaribale.URL + StaticVaribale.PATH.categogry.getList, {params: params})
  }

  getListNoAuth(level: any = null) : Observable<any> {
    let params = new HttpParams();
    if(level != null)
        params = params.append('Level', level);
    return this.http.get(StaticVaribale.URL + StaticVaribale.PATH.categogry.getListnoauth, {params: params})
  }

  getpaging(request: any) : Observable<any> {
    let params = new HttpParams();
    params = params.append('Page', request.Page);
    params = params.append('Limit', request.Limit);
    params = params.append('Keyword', request.Keyword);
    params = params.append('Level', request.Level);

    return this.http.get(StaticVaribale.URL + StaticVaribale.PATH.categogry.getpaging, {params});
  }

  create(data: any) : Observable<any> {
    let dataTemp = Object.assign({}, data);
    const body = JSON.stringify(dataTemp);
    return this.http.post(StaticVaribale.URL + StaticVaribale.PATH.categogry.create, body, StaticVaribale.httpOptions);
  }

  update(data: any) : Observable<any> {
    let dataTemp = Object.assign({}, data);
    const body = JSON.stringify(dataTemp);
    return this.http.put(StaticVaribale.URL + StaticVaribale.PATH.categogry.update, body, StaticVaribale.httpOptions);
  }

  delete(id: any) : Observable<any> {
    return this.http.delete(StaticVaribale.URL + StaticVaribale.PATH.categogry.delete + '/' + id);
  }
}
