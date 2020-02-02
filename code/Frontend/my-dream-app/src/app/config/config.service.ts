import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Tipos } from "./tipos.enum";
import { BehaviorSubject } from "rxjs";
import { Productsmodel } from "./../models/productsmodel";
import { Productsmodel2 } from '../products/addset/addset.component';
@Injectable({
  providedIn: 'root'
})

export class ConfigService {

  private productsSource = new BehaviorSubject<Productsmodel>(new Productsmodel(0, '', '', 0, 0, 0, 0, 0, 0));
  private _listproductsSource = new BehaviorSubject<Productsmodel[]>([]);
  private _todos = new BehaviorSubject<any[]>([]);

  // private productsSource = new BehaviorSubject<Productsmodel>(null);
  productsData = this.productsSource.asObservable()
  listproductsData = this._listproductsSource.asObservable()

  constructor(private http: HttpClient) { }

  // configUrl = 'https://jsonplaceholder.typicode.com/posts/1/comments';
  uriResources = 'http://10.211.55.3/poshubdev/api/';
  // uriResources = 'http://localhost/poshubdev/api/';

  changeProductsData(productsargs: Productsmodel) {
    this.productsSource.next(productsargs);
  }

  changeListProductsData(listproductsargs: []) {
    this._listproductsSource.next(listproductsargs);
  }

  //TODO
  // check error in api rest when exist barcode
  changeListProductsDataAdd(productsargs: Productsmodel) {
    let found = this._listproductsSource.getValue().find(element => element.idproducts == productsargs.idproducts);
    if (found) {
      let tmplist = this._listproductsSource.getValue();
      tmplist.splice(tmplist.indexOf(found), 1);
      tmplist = tmplist.concat(productsargs);
      this._listproductsSource.next(tmplist);

    }
    else {
      this._listproductsSource.next(this._listproductsSource.getValue().concat(productsargs));
    }
  }


  private extractData(res: Response) {
    let body = res;
    // return body || {};
    return body || {};

  }
  // getConfig(): Observable<any> {
  //   return this.http.get(this.configUrl).pipe(map(this.extractData));
  // }

  Get(serviceName: String): Observable<any> {
    return this.http.get(`${this.uriResources}${serviceName}`).pipe(map(this.extractData));
  }


  Make(serviceName: String, tipo: any, data: any): Observable<any> {

    //TODO
    // control exception library
    // NLOG for typescript
    switch (tipo) {
      case Tipos.POST:
        return this.http.post(`${this.uriResources}${serviceName}`, data).pipe(map(this.extractData));

      case Tipos.PUT:
        return this.http.put(`${this.uriResources}${serviceName}`, data).pipe(map(this.extractData));

      case Tipos.PATCH:
        return this.http.patch(`${this.uriResources}${serviceName}`, data).pipe(map(this.extractData));

      case Tipos.DELETE:
        return this.http.delete(`${this.uriResources}${serviceName}`).pipe(map(this.extractData));

      default:
        return null;
    }
  }
}
