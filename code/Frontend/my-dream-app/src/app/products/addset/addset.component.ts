import { Component, OnInit } from '@angular/core';
import { Productsmodel } from "./../../models/productsmodel";
import { Tipos } from "./../../config/tipos.enum";
import { CSTATUS } from "./../../config/tipos.enum";
import { ConfigService } from "./../../config/config.service";

export class Productsmodel2 {
  // idproducts: number;
  // name: String;
  constructor(public idproducts: number, public name: String) { }
}

@Component({
  selector: 'products-addset',
  templateUrl: './addset.component.html',
  styleUrls: ['./addset.component.css']
})
export class AddsetComponent implements OnInit {
  // setup initial
  //model: any ;   
  model = new Productsmodel(0, '', '', 0, 0, 0, 0, 0, 0);
  selected = '2';
  liststatus = CSTATUS;
  constructor(protected service: ConfigService) { }

  ngOnInit() {
    this.service.productsData.subscribe(res => {
      this.model = res;
      this.selected = this.model.idcstatus > 0 ? this.model.idcstatus.toString() : '1';
    });

  }

  onEventSelection(event) {
    // console.dir(event);
    this.selected = event;
    this.model.idcstatus = event;
  }

  onSaveForm() {
    let tmpmethod: Tipos;
    let tmpendpoint: String = 'products';
    if (this.model.idproducts > 0) {
      tmpmethod = Tipos.PUT
      tmpendpoint = `${tmpendpoint}/${this.model.idproducts}`
    } else {
      tmpmethod = Tipos.POST
    }

    this.service.Make(tmpendpoint, tmpmethod, this.model).subscribe((data) => {
      if (data.response) {
        console.dir(data);
      }
    }, (error) => {
      console.dir(error);
    });
  }

}
