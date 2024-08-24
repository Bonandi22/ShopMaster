import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ConfigService {
  private config = {
    apiUrl: 'http://localhost:3000'
  };

  get apiUrl(): string {
    return this.config.apiUrl;
  }

  // Outros métodos de configuração podem ser adicionados aqui
}
