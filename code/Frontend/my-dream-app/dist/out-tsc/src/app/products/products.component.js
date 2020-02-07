import * as tslib_1 from "tslib";
import { Component } from '@angular/core';
let ProductsComponent = class ProductsComponent {
    constructor(service) {
        this.service = service;
        this.products = [];
    }
    ngOnInit() {
        this.getProducts();
    }
    getProducts() {
        this.products = [];
        this.service.getConfig().subscribe((data) => {
            console.dir(data);
            this.products = data;
        });
    }
};
ProductsComponent = tslib_1.__decorate([
    Component({
        selector: 'app-products',
        templateUrl: './products.component.html',
        styleUrls: ['./products.component.css']
    })
], ProductsComponent);
export { ProductsComponent };
//# sourceMappingURL=products.component.js.map