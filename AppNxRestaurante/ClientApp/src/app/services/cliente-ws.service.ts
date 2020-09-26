import { Injectable } from '@angular/core';
import { Console } from 'console';
import { map } from 'rxjs/operators';
import { ClienteModel } from '../model/Cliente';
import { RespuestaModel } from '../model/Respuesta';
import { WebApiService } from './web-api.service';

@Injectable()
export class ClienteWsService {
  urlCliente = "api/Clientes"
  constructor(private ws: WebApiService) { }

  obtenerCliente() {
    let response = new RespuestaModel();
    this.ws.wsGet(this.urlCliente).subscribe(res => {
      console.log(res);

    }, err => {
      response.respuesta = "Upps, no fue posible procesar la solicitud.";
      response.error = true;

    });

  }

  obtenerTodos(): RespuestaModel {
    let response = new RespuestaModel();

    this.ws.wsGet(this.urlCliente).pipe(map(res => res as object as ClienteModel[])).subscribe(res => {
      response.respuesta = "ok";
      response.error = false;
      response.objeto = res;
    }, err => {
      response.respuesta = "Upps, no fue posible procesar el registro.";
      response.error = true;

    });
    return response;
  }

  obtenerTodos2(){
    return this.ws.wsGet(this.urlCliente).pipe(map(res => res as object as ClienteModel[]));
  }



  registrarCliente(cliente: ClienteModel): RespuestaModel {
    let url = this.urlCliente;
    let response = new RespuestaModel();

    this.ws.wsPost(url, cliente).subscribe(res => {
      console.log(res);
      if (res["idCliente"]) {
        response.respuesta = "Se creo el usuario exitosamente";
        response.error = false;
      }
    }, err => {
      response.respuesta = "Upps, no fue posible procesar el registro.";
      response.error = true;

    });

    return response;
  }

}
