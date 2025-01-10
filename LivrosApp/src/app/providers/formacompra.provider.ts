import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { IFormaCompraDto } from '../models/dtos/formacompra.dto.model';
import { IRetornoAPI } from '../models/Infraestrutura/retorno.api.model';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class FormaCompraProvider {
  constructor(
    private readonly http: HttpClient
  ) {}

  headers: HttpHeaders = new HttpHeaders();

  private routePrefix = `${environment.apiUrl}/FormaCompra`;

  GetFormaCompras(): Observable<IRetornoAPI<Array<IFormaCompraDto>>> {
    return this.http
      .get<IRetornoAPI<Array<IFormaCompraDto>>>(this.routePrefix);
  }

  EditarFormaCompra(FormaCompraRequest: IFormaCompraDto): Observable<IRetornoAPI<any>> {
    return this.http
      .put<any>(this.routePrefix, FormaCompraRequest)      
  }

  CadastrarFormaCompra(FormaCompraRequest: IFormaCompraDto): Observable<IRetornoAPI<any>> {
    return this.http
      .post<any>(this.routePrefix, FormaCompraRequest)      
  }

  ApagarFormaCompra(idFormaCompra: number): Observable<IRetornoAPI<any>> {
    return this.http
      .delete<any>(this.routePrefix, {body: {Id: idFormaCompra}});      
  } 
}
