import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthService } from '../services/auth.storage.service';

@Injectable({
  providedIn: 'root',
})

export class RedirectGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router) {}

  canActivate(): boolean {
    const token = this.authService.getAccessToken();

    if (token) {
      this.router.navigate(['/']);
    } else {
      this.router.navigate(['/login']);
    }

    return false;
  }
}
