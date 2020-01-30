import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Tipos } from "./tipos.enum";

@Injectable({
  providedIn: 'root'
})
export class ConfigService {

  constructor(private http: HttpClient) {}

  // configUrl = 'https://jsonplaceholder.typicode.com/posts/1/comments';
  // uriResources = 'http://10.211.55.3/poshubdev/api/';
  uriResources = 'http://localhost/poshubdev/api/';

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

  Get(serviceName:String):Observable<any> {
    return this.http.get(`${this.uriResources}${serviceName}`).pipe(map(this.extractData));
  }

  Post(serviceName:String,data:any):Observable<any> {
    return this.http.post(`${this.uriResources}${serviceName}`,data).pipe(map(this.extractData));
  }

  Make(serviceName:String,tipo:Tipos , data:any):Observable<any> {
    switch (tipo) {
      case Tipos.POST:
        return this.http.post(`${this.uriResources}${serviceName}`,data).pipe(map(this.extractData));
        break;

        case Tipos.PATCH:
        return this.http.patch(`${this.uriResources}${serviceName}`,data).pipe(map(this.extractData));
        break;

        case Tipos.PATCH:
          return this.http.patch(`${this.uriResources}${serviceName}`,data).pipe(map(this.extractData));
          break;
    
      default:
        break;
    }
  }
}
