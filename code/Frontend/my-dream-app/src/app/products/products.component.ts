import { Component, OnInit } from '@angular/core';
import { ConfigService } from "./../config/config.service";

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})

export class ProductsComponent implements OnInit {

  public products: any = [];

  constructor(protected service: ConfigService) { }

  ngOnInit() {
    this.getProducts();
  }
  getProducts() {
    this.products = [];
    //  this.service.getConfig().subscribe((data: {}) => {
    this.service.Get('products').subscribe((data) => {
      if (data.response) {
        this.products = data.result.slice();
      }

      
    }, (error) => {
      console.dir(error);
      alert(error);
    });
  }


}
