import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CatalogComponent } from './containers/catalog/catalog.component';
import { ProductListComponent } from './components/product-list/product-list.component';
import { ProductItemComponent } from './components/product-item/product-item.component';
import { HttpClientModule } from '@angular/common/http';
import { CatalogRoutingModule } from './catalog-routing.module';

@NgModule({
  declarations: [
    CatalogComponent,        // Declara o container principal do catálogo
    ProductListComponent,    // Declara o componente para listar produtos
    ProductItemComponent     // Declara o componente para exibir item individual
  ],
  imports: [
    CommonModule,            // Importa funcionalidades comuns do Angular
    CatalogRoutingModule,
    HttpClientModule         // Importa o módulo para realizar chamadas HTTP
  ]
})
export class CatalogModule { }
