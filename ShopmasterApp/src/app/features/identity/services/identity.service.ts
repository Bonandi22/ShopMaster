import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { RegisterDto } from '../models/register.dto';
import { AuthResponse } from '../models/auth-response.model';
import { LoginDto } from '../models/login.dto';

@Injectable({
  providedIn: 'root'
})
export class IdentityService {
  private apiUrl = 'https://localhost:7277/api/Identity';

  constructor(private http: HttpClient) { }

  register(data: RegisterDto): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${this.apiUrl}/register`, data);
  }

  login(data: LoginDto): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${this.apiUrl}/login`, data);
  }
}
