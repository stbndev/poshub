import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Tipos } from "./tipos.enum";

@Injectable({
  providedIn: 'root'
})

export class ConfigService {

  constructor(private http: HttpClient) { }

  // configUrl = 'https://jsonplaceholder.typicode.com/posts/1/comments';
  uriResources = 'http://10.211.55.3/poshubdev/api/';
  // uriResources = 'http://localhost/poshubdev/api/';

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };

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

  // Call(serviceName: String, tipo: Tipo, data: any): Observable<any> {

  //   let tmpresponse: any;
  //   switch (tipo) {

  //     case Tipo.POST:
  //       tmpresponse = this.http.post(`${this.uriResources}${serviceName}`, data).pipe(map(this.extractData));
  //       break;

  //     case Tipo.PUT:
  //       tmpresponse = this.http.post(`${this.uriResources}${serviceName}`, data).pipe(map(this.extractData));
  //       break;

  //     case Tipo.DELETE:
  //       tmpresponse = this.http.post(`${this.uriResources}${serviceName}`, data).pipe(map(this.extractData));
  //       break;

  //     default:
  //       throw "not found enum tipo";
  //       break;
  //   }
  //   return tmpresponse;
  // }

  Make(serviceName: String, tipo:any, data: any): Observable<any> {

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
