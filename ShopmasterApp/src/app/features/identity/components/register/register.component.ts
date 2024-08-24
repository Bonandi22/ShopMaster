import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  userName = '';
  password = '';

  @Output() register = new EventEmitter<{ userName: string; password: string }>();

  onRegister(): void {
    this.register.emit({ userName: this.userName, password: this.password });
  }
}
