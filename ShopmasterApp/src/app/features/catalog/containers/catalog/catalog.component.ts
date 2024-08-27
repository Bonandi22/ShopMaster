import { Component, OnInit } from '@angular/core';
import { CatalogService } from '../../services/catalog.service';
import { Product } from '../../models/product.model';

@Component({
  selector: 'app-catalog',
  templateUrl: './catalog.component.html',
  styleUrls: ['./catalog.component.css'],
})
export class CatalogComponent implements OnInit {
  products: Product[] = [];

  constructor(private catalogService: CatalogService) { }

  ngOnInit(): void {
    console.log('CatalogComponent initialized');
    this.loadProducts();
  }

  loadProducts(): void {
    this.catalogService.getProducts().subscribe({
      next: (products) => {
        this.products = products;
        console.log('Products loaded in CatalogComponent:', this.products);
      },
      error: (err) => {
        console.error('Error loading products', err);
      }
    });
  }
}
