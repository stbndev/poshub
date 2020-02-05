import * as tslib_1 from "tslib";
import { Injectable } from '@angular/core';
import { HttpHeaders } from '@angular/common/http';
import { map } from 'rxjs/operators';
let ConfigService = class ConfigService {
    constructor(http) {
        this.http = http;
        this.configUrl = 'https://jsonplaceholder.typicode.com/posts/1/comments';
        this.httpOptions = {
            headers: new HttpHeaders({
                'Content-Type': 'application/json'
            })
        };
    }
    extractData(res) {
        let body = res;
        return body || {};
    }
    getConfig() {
        return this.http.get(this.configUrl).pipe(map(this.extractData));
    }
};
ConfigService = tslib_1.__decorate([
    Injectable({
        providedIn: 'root'
    })
], ConfigService);
export { ConfigService };
//# sourceMappingURL=config.service.js.map