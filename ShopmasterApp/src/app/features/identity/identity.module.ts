import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IdentityRoutingModule } from './identity-routing.module';
import { IdentityComponent } from './containers/identity/identity.component';
import { RegisterComponent } from './components/register/register.component';
import { LoginComponent } from './components/login/login.component';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    IdentityComponent,
    RegisterComponent,
    LoginComponent
  ],
  imports: [
    CommonModule,
    IdentityRoutingModule,
    FormsModule
  ]
})
export class IdentityModule { }
