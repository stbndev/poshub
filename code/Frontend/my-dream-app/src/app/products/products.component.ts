import { Component, OnInit } from '@angular/core';
import { ConfigService } from "./../config/config.service";
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Tipos } from "./../config/tipos.enum";
import { Productsmodel } from "./../models/productsmodel";
@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})

export class ProductsComponent implements OnInit {

  products: any = [];
  // model: Productsmodel;
  model: Productsmodel;

  constructor(protected service: ConfigService) { }

  ngOnInit() {
    //model = new Productsmodel();
    this.getProducts();

    this.service.productsData.subscribe(res => {
      this.model = res;
    });

    this.service.listproductsData.subscribe(res => {
      this.products = res;
    });

  }

  onSelect(event, item) {
    var tmp = Object.assign(this.model, item);
    this.service.changeProductsData(tmp);
  }

  getProducts() {
    this.products = [];
    //  this.service.getConfig().subscribe((data: {}) => {
    this.service.Get('products').subscribe((data) => {
      if (data.response) {
        // this.products = data.result.slice();
        this.service.changeListProductsData(data.result.slice());
      }
    }, (error) => {
      console.dir(error);
      alert(error);
    });
  }

}
