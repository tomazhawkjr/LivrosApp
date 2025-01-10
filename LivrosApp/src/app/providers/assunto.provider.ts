import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { IAssuntoDto } from '../models/dtos/assunto.dto.model';
import { IRetornoAPI } from '../models/Infraestrutura/retorno.api.model';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class AssuntoProvider {
  constructor(
    private readonly http: HttpClient
  ) {}

  headers: HttpHeaders = new HttpHeaders();

  private routePrefix = `${environment.apiUrl}/Assunto`;

  GetAssuntos(): Observable<IRetornoAPI<Array<IAssuntoDto>>> {
    return this.http
      .get<IRetornoAPI<Array<IAssuntoDto>>>(this.routePrefix);
  }

  EditarAssunto(assuntoRequest: IAssuntoDto): Observable<IRetornoAPI<any>> {
    return this.http
      .put<any>(this.routePrefix, assuntoRequest)      
  }

  CadastrarAssunto(assuntoRequest: IAssuntoDto): Observable<IRetornoAPI<any>> {
    return this.http
      .post<any>(this.routePrefix, assuntoRequest)      
  }

  ApagarAssunto(idAssunto: number): Observable<IRetornoAPI<any>> {
    return this.http
      .delete<any>(this.routePrefix, {body: {Id: idAssunto}});      
  } 
}
