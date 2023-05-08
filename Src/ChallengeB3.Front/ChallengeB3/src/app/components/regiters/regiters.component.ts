import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-regiters',
  templateUrl: './regiters.component.html',
  styleUrls: ['./regiters.component.css']
})
export class RegitersComponent implements OnInit {

  formulario: any;
  tituloFormulario: string;

  constructor() { }

  ngOnInit() {
    this.tituloFormulario = "Novo Registro";
    this.formulario = new FormGroup({
      description: new FormControl(null),
      status: new FormControl(null),
      date: new FormControl(null)
    });
  }
}