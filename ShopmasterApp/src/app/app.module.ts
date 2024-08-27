import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { OrdersModule } from './features/orders/orders.module';
import { CatalogModule } from './features/catalog/catalog.module';
import { GuardsModule } from './core/guards/guards.module';
import { InterceptorsModule } from './core/interceptors/interceptors.module';
import { ServicesModule } from './core/services/services.module';
import { SharedModule } from './shared/shared.module';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';

@NgModule({
  declarations: [
    AppComponent,

  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    OrdersModule,
    CatalogModule,
    GuardsModule,
    InterceptorsModule,
    ServicesModule,
    SharedModule
  ],
  providers: [
    provideAnimationsAsync()
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
