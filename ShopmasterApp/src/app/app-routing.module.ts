import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './shared/components/home/home.component';

const routes: Routes = [
  // Rota para a Home Page
  { path: '', component: HomeComponent }, // Página principal será a HomeComponent

  // Rota para o módulo Catalog com lazy loading
  { path: 'catalog', loadChildren: () => import('./features/catalog/catalog.module').then(m => m.CatalogModule) },

  // Rota para o módulo Orders com lazy loading
  { path: 'orders', loadChildren: () => import('./features/orders/orders.module').then(m => m.OrdersModule) },

  // Rota coringa para redirecionar páginas não encontradas
  { path: '**', redirectTo: '' } // Redireciona para a Home caso a rota não seja encontrada
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
