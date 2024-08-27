import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { Product } from '../../models/product.model';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnChanges {
  @Input() products: Product[] = [];

  constructor() { }

  ngOnChanges(changes: SimpleChanges) {
    if (changes['products']) {
      console.log('Products in ProductListComponent:', this.products);
    }
  }
  getFullImagePath(imagePath: string): string {
    const normalizedPath = imagePath.replace(/\\/g, '/');
    return `https://localhost:7014/${normalizedPath}`;
  }
  addToCart(product: Product): void {
    console.log('Produto adicionado ao carrinho:', product);
    // adicione a lógica para adicionar o produto ao carrinho aqui
  }

  favoriteProduct(product: Product): void {
    console.log('Produto favoritado:', product);
    // adicione a lógica para favoritar o produto aqui
  }

  viewProductDetails(product: Product): void {
    console.log('Detalhes do produto:', product);
    // adicione a lógica para exibir os detalhes do produto aqui
  }
}
