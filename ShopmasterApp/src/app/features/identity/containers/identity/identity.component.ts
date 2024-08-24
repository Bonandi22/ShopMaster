import { Component } from '@angular/core';
import { IdentityService } from '../../services/identity.service';
import { LoginDto } from '../../models/login.dto';
import { RegisterDto } from '../../models/register.dto';

@Component({
  selector: 'app-identity',
  templateUrl: './identity.component.html',
  styleUrls: ['./identity.component.css']
})
export class IdentityComponent {
  isLoading = false;
  errorMessage: string | null = null;

  constructor(private identityService: IdentityService) { }

  login(userName: string, password: string): void {
    this.isLoading = true;
    const loginDto: LoginDto = { userName, password };
    this.identityService.login(loginDto).subscribe({
      next: (response) => {
        // Sucesso no login
        console.log('Login successful', response);
        this.isLoading = false;
        this.storeToken(response.token);
        // Redirecionar ou atualizar o estado do aplicativo
      },
      error: (error) => {
        // Erro no login
        console.error('Login failed', error);
        this.errorMessage = 'Login failed. Please check your credentials.';
        this.isLoading = false;
      }
    });
  }

  register(userName: string, password: string): void {
    this.isLoading = true;
    const registerDto: RegisterDto = { userName, password };
    this.identityService.register(registerDto).subscribe({
      next: (response) => {
        // Sucesso no registro
        console.log('Registration successful', response);
        this.isLoading = false;
        this.storeToken(response.token);
        // Redirecionar ou atualizar o estado do aplicativo
      },
      error: (error) => {
        // Erro no registro
        console.error('Registration failed', error);
        this.errorMessage = 'Registration failed. Please try again.';
        this.isLoading = false;
      }
    });
  }

  private storeToken(token: string): void {
    // Armazenar o token no localStorage
    localStorage.setItem('authToken', token);
  }

  logout(): void {
    localStorage.removeItem('authToken');
    // Redirecionar ou atualizar o estado do aplicativo
  }
}
