import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { of } from 'rxjs/observable/of';
import { catchError, debounceTime, distinctUntilChanged, switchMap, tap } from 'rxjs/operators';
import { FacturaModel } from '../../../model/Factura';
import { FacturaWsService } from '../../../services/factura-ws.service';
import { WikipediaService } from '../../../services/wikipedia.service';

@Component({
  selector: 'app-registrar-factura',
  templateUrl: './registrar-factura.component.html',
  styleUrls: ['./registrar-factura.component.css']
})
export class RegistrarFacturaComponent implements OnInit {
  formFactura: FormGroup;
  submitted = false;
  titulo="Factura";
  subtitulo = "Aquí puedes registrar tus facturas";


  //serc
  model: any;
  searching = false;
  searchFailed = false;

  constructor(private fb: FormBuilder, private facturaWs:FacturaWsService, private _service:WikipediaService) { }
 
  ngOnInit() {
    this.formFactura = this.fb.group({
      IdCliente: ['', Validators.compose([Validators.required, Validators.minLength(4), Validators.maxLength(20)])],
      IdMesa: ['', Validators.compose([Validators.required, Validators.minLength(3), Validators.maxLength(50)])],
      IdCamarero: ['', Validators.compose([Validators.required, Validators.minLength(3), Validators.maxLength(50)])],
      FFactura: ['', Validators.compose([Validators.required, Validators.minLength(3), Validators.maxLength(50)])],
      //VObservacion: ['', Validators.compose([Validators.nullValidator])]
    });

  }

  get f() { return this.formFactura.controls; }

  send() {
    this.submitted = true;

    if (this.formFactura.invalid) {
      return;
    }

    //alert('SUCCESS!! :-)\n\n' + JSON.stringify(this.formCliente.value));
    let cliente = this.formFactura.value as FacturaModel;
   // this.facturaWs.registrarFactura(cliente);
  }

  search = (text$: Observable<string>) =>
    text$.pipe(
      debounceTime(300),
      distinctUntilChanged(),
      tap(() => this.searching = true),
      switchMap(term =>
        this._service.search(term).pipe(
          tap(() => this.searchFailed = false),
          catchError(() => {
            this.searchFailed = true;
            return of([]);
          }))
      ),
      tap(() => this.searching = false)
    )
}


