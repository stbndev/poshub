import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})
export class ConfigService {
 
  constructor(private http:HttpClient) {

  }

  configUrl = 'https://jsonplaceholder.typicode.com/posts/1/comments';

  getConfig() {
   return this.http.get(this.configUrl);
  }
}
