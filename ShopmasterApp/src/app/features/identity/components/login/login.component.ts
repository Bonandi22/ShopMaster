import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  userName = '';
  password = '';

  @Output() login = new EventEmitter<{ userName: string; password: string }>();

  onLogin(): void {
    this.login.emit({ userName: this.userName, password: this.password });
  }
}
