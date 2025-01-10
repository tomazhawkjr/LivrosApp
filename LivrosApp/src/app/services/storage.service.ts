import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class StorageService {
 
  setItem(key: string, value: any): void {
    let serializedValue: string;

    if (typeof value === 'object' && value !== null) {
      serializedValue = JSON.stringify(value);
    } else {
      serializedValue = value.toString();
    }
  
    localStorage.setItem(key, serializedValue);
  }

  getItem<T>(key: string): T | null {
    const serializedValue = localStorage.getItem(key);
    if (!serializedValue) {
      return null;
    }
    try {
      return JSON.parse(serializedValue) as T;
    } catch {
      return null;
    }
  }

  getRawItem(key: string): string | null {
    return localStorage.getItem(key);
  }

  removeItem(key: string): void {
    localStorage.removeItem(key);
  }

  clear(): void {
    localStorage.clear();
  }
}
