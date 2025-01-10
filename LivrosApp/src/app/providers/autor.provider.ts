import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { IAutorDto } from '../models/dtos/autor.dto.model';
import { IRetornoAPI } from '../models/Infraestrutura/retorno.api.model';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class AutorProvider {
  constructor(
    private readonly http: HttpClient
  ) {}

  headers: HttpHeaders = new HttpHeaders();

  private routePrefix = `${environment.apiUrl}/Autor`;

  GetAutores(): Observable<IRetornoAPI<Array<IAutorDto>>> {
    return this.http
      .get<IRetornoAPI<Array<IAutorDto>>>(this.routePrefix);
  }

  EditarAutor(autorRequest: IAutorDto): Observable<IRetornoAPI<any>> {
    return this.http
      .put<any>(this.routePrefix, autorRequest)      
  }

  CadastrarAutor(autorRequest: IAutorDto): Observable<IRetornoAPI<any>> {
    return this.http
      .post<any>(this.routePrefix, autorRequest)      
  }

  ApagarAutor(idAutor: number): Observable<IRetornoAPI<any>> {
    return this.http
      .delete<any>(this.routePrefix, {body: {Id: idAutor}});      
  } 
}
