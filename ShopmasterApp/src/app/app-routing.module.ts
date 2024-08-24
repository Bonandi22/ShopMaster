import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  // Rota para o módulo Catalog
  { path: 'catalog', loadChildren: () => import('./features/catalog/catalog.module').then(m => m.CatalogModule) },

  // Rota para o módulo Orders
  { path: 'orders', loadChildren: () => import('./features/orders/orders.module').then(m => m.OrdersModule) },

  // Rota padrão para redirecionar para o catálogo ou página inicial
  { path: '', redirectTo: '/catalog', pathMatch: 'full' },

  // Rota coringa para redirecionar páginas não encontradas
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
