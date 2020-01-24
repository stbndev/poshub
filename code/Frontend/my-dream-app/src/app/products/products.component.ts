import { Component, OnInit } from '@angular/core';
import { ConfigService } from "./../config/config.service";
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ProductsAddSetComponent } from "./../products-add-set/products-add-set.component";
@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})

export class ProductsComponent implements OnInit {

  public products: any = [];

  constructor(public dialog: MatDialog, protected service: ConfigService) { }

  ngOnInit() {
    this.getProducts();
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
