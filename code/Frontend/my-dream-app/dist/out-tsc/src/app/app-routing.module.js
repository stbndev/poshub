import * as tslib_1 from "tslib";
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { ProductsComponent } from "./products/products.component";
const routes = [
    // { path:  '', redirectTo:  'mainmenu', pathMatch:  'full' },
    // { path:  'products', redirectTo:  'product', pathMatch:  'full' },
    {
        path: 'products',
        component: ProductsComponent,
        data: { title: 'Product List' }
    },
];
let AppRoutingModule = class AppRoutingModule {
};
AppRoutingModule = tslib_1.__decorate([
    NgModule({
        imports: [RouterModule.forRoot(routes)],
        exports: [RouterModule]
    })
], AppRoutingModule);
export { AppRoutingModule };
//# sourceMappingURL=app-routing.module.js.map