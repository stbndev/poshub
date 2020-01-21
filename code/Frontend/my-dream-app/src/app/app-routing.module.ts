import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ProductsComponent } from "./products/products.component";

const routes: Routes = [
  // { path:  '', redirectTo:  'mainmenu', pathMatch:  'full' },
  // { path:  'products', redirectTo:  'product', pathMatch:  'full' },
  {
    path: 'products',
    component: ProductsComponent,
    data: { title: 'Product List' }
  },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {

}
