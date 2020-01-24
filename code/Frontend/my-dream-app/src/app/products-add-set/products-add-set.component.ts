import { Component, OnInit } from '@angular/core';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA ,} from '@angular/material/dialog';

@Component({
  selector: 'app-products-add-set',
  templateUrl: './products-add-set.component.html',
  styleUrls: ['./products-add-set.component.css']
})
export class ProductsAddSetComponent implements OnInit {

  constructor(public dialogRef: MatDialogRef<ProductsAddSetComponent>) { }

  ngOnInit() {
  }

}
