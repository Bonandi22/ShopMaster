import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { IdentityComponent } from './containers/identity/identity.component';

const routes: Routes = [
  { path: 'Identity', component: IdentityComponent },
  // Se você quiser rotas específicas para RegisterComponent e LoginComponent, adicione-as aqui
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class IdentityRoutingModule { }
