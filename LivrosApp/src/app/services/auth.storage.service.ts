import { Injectable } from '@angular/core';
import { StorageService } from './storage.service'; 
import { ITokenDto } from '../models/dtos/login/token.dto.model';

@Injectable({
  providedIn: 'root',
})
export class AuthService {

  private static readonly KEY_ACCESS_TOKEN = 'ACCESS_TOKEN';
  private static readonly KEY_TOKEN = 'TOKEN';

  constructor(private storageService: StorageService) {}

  private setAccessToken(token: string): void {
    this.storageService.setItem(AuthService.KEY_ACCESS_TOKEN, token);
  }

  getAccessToken(): string | null {
    return this.storageService.getRawItem(AuthService.KEY_ACCESS_TOKEN);
  }

  setToken(token: ITokenDto): void {
    this.storageService.setItem(AuthService.KEY_TOKEN, token);
    this.setAccessToken(token.access_token);
  }

  getToken(): ITokenDto | null {
    return this.storageService.getItem<ITokenDto>(AuthService.KEY_TOKEN);
  }

  clearTokens(): void {
    this.storageService.removeItem(AuthService.KEY_ACCESS_TOKEN);
    this.storageService.removeItem(AuthService.KEY_TOKEN);
  }
}
