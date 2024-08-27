import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './components/home/home.component';
import { HeaderComponent } from './components/header/header.component';
import { FooterComponent } from './components/footer/footer.component';
import { RouterModule } from '@angular/router';
import { NavbarComponent } from './components/navbar/navbar.component';
import { CatalogModule } from '../features/catalog/catalog.module';
import { MatMenuModule } from '@angular/material/menu';
import { MatButtonModule } from '@angular/material/button';
import { AvatarComponent, TextColorDirective } from '@coreui/angular';
import { MatIconModule } from '@angular/material/icon';




@NgModule({
  declarations: [
    HomeComponent,
    HeaderComponent,
    FooterComponent,
    NavbarComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    CatalogModule,
    MatButtonModule,
    MatMenuModule,
    TextColorDirective,
    AvatarComponent,
    MatIconModule


  ],
  exports: [
    HomeComponent,
    HeaderComponent,
    FooterComponent,
    NavbarComponent
  ]
})
export class SharedModule { }
