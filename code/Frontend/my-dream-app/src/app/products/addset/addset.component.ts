import { Component, OnInit } from '@angular/core';
import { Productsmodel } from "./../../models/productsmodel";
import { Tipos } from "./../../config/tipos.enum";
import { ConfigService } from "./../../config/config.service";

export interface CSTATUS {
  value: string;
  viewValue: string;
}

@Component({
  selector: 'products-addset',
  templateUrl: './addset.component.html',
  styleUrls: ['./addset.component.css']
})
export class AddsetComponent implements OnInit {
  // setup initial
  model = new Productsmodel();

  liststatus: CSTATUS[] = [
    {value: '1', viewValue: 'ACTIVO'},
    {value: '2', viewValue: 'INACTIVO'},
    {value: '3', viewValue: 'ELIMINADO'}
  ];

  constructor(protected service: ConfigService) { }

  ngOnInit() {
  }

  onEventSelection(event){
    this.model.idcstatus = event.value
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
