import { Component, OnInit } from '@angular/core';
import { ConfigService } from "./../config/config.service";
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ProductsAddSetComponent } from "./../products-add-set/products-add-set.component";
import { Productsvm } from '../viewmodels/productsvm';
import { Tipo } from "./../config/Enum.Tipo";
@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})

export class ProductsComponent implements OnInit {

  // public products: any = [];
  products: any = [];
  // model = new Productsvm(0,'','',0,0,0,0,0,0);
  model = new Productsvm();
  constructor(public dialog: MatDialog, protected service: ConfigService) { }

  ngOnInit() {
    this.getProducts();
  }

  onSelect(event, item) {
    Object.assign(this.model, item);
  }

  onSaveForm() {
    
    let tmptipo = this.model.idproducts == 0 ? Tipo.POST : Tipo.PUT;

    if(this.model.idproducts )
    this.model.idproducts;
    
    this.service.Call('products', null , null).subscribe((data) => {
      if (data.response) {
        console.dir(data);
      }
    }, (error) => {
      console.dir(error);
    });
  }

  add(): boolean {

    return true;
  }
  openDialog(): void {
    const dialogRef = this.dialog.open(ProductsAddSetComponent,
      {
        width: '1024px',
        height: '720px',
      }
    );

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      // this.animal = result;
    });
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
