import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CatalogComponent } from './containers/catalog/catalog.component';
import { ProductListComponent } from './components/product-list/product-list.component';
import { HttpClientModule } from '@angular/common/http';
import { CatalogRoutingModule } from './catalog-routing.module';
import { ProductFormComponent } from './components/product-form/product-form.component';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    CatalogComponent,
    ProductListComponent,
    ProductFormComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    CatalogRoutingModule,
    HttpClientModule
  ],
  exports: [
    CatalogComponent,
    ProductListComponent,
  ]
})
export class CatalogModule { }
