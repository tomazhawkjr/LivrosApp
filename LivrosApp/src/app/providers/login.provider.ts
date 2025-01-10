import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { IRetornoAPI } from '../models/Infraestrutura/retorno.api.model';
import { Observable } from 'rxjs';
import { ITokenDto } from '../models/dtos/login/token.dto.model';
import { ILoginRequest } from '../models/Requests/login.request.model';

@Injectable({ providedIn: 'root' })
export class LoginProvider {
  constructor(
    private readonly http: HttpClient
  ) {}

  headers: HttpHeaders = new HttpHeaders();

  private routePrefix = `${environment.apiUrl}/Account`;

  Login(email: string, password: string): Observable<IRetornoAPI<ITokenDto>> {

    var loginRequest : ILoginRequest = {
      clientId: 'Web',
      grantType: 'password',
      email,
      password
    }

    return this.http
      .post<IRetornoAPI<ITokenDto>>(`${this.routePrefix}/Login`, loginRequest);
  }
}
