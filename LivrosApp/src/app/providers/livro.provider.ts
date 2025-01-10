import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ILivroRequest } from '../models/Requests/livro.request.model';
import { ILivroDto } from '../models/dtos/livro.dto.model';
import { IRetornoAPI } from '../models/Infraestrutura/retorno.api.model';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class LivroProvider {
  constructor(
    private readonly http: HttpClient
  ) {}

  headers: HttpHeaders = new HttpHeaders();

  private routePrefix = `${environment.apiUrl}/Livro`;

  GetLivros(): Observable<IRetornoAPI<Array<ILivroDto>>> {
    return this.http
      .get<IRetornoAPI<Array<ILivroDto>>>(this.routePrefix);
  }

  EditarLivro(livroRequest: ILivroRequest): Observable<IRetornoAPI<any>> {
    return this.http
      .put<any>(this.routePrefix, livroRequest)      
  }

  CadastrarLivro(livroRequest: ILivroRequest): Observable<IRetornoAPI<any>> {
    return this.http
      .post<any>(this.routePrefix, livroRequest)      
  }

  ApagarLivro(idLivro: number): Observable<IRetornoAPI<any>> {
    return this.http
      .delete<any>(this.routePrefix, {body: {Id: idLivro}});      
  } 

  BaixarRelatorio(): Observable<Blob> {
    return this.http
      .get<any>(`${this.routePrefix}/EmitirRelatorio`, {responseType: 'blob' as 'json' });      
  } 
}
