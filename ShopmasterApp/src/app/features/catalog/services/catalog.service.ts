import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Product } from '../models/product.model';

@Injectable({
  providedIn: 'root'
})
export class CatalogService {
  private apiUrl = 'https://localhost:7014/api/Products'; 

  constructor(private http: HttpClient) { }

  // Obtém todos os produtos
  getProducts(): Observable<Product[]> {
    console.log('Requesting products from API');
    return this.http.get<Product[]>(this.apiUrl).pipe(
      catchError(error => {
        console.error('Error getting products', error);
        return throwError(() => new Error('Error getting products'));
      })
    );
  }


  // Obtém um produto pelo ID
  getProductById(id: string): Observable<Product> {
    const url = `${this.apiUrl}/${id}`;
    return this.http.get<Product>(url);
  }

  // Cria um novo produto
  createProduct(product: Product, image?: File): Observable<Product> {
    const formData = new FormData();
    formData.append('name', product.name ?? '');
    formData.append('description', product.description ?? '');
    formData.append('price', product.price.toString());
    formData.append('categoryId', product.categoryId.toString());
    if (image) {
      formData.append('image', image);
    }

    return this.http.post<Product>(this.apiUrl, formData).pipe(
      catchError(error => {
        console.error('Error creating product', error);
        return throwError(() => new Error('Error creating product'));
      })
    );
  }

  // Atualiza um produto existente
  updateProduct(id: string, product: Product): Observable<void> {
    const url = `${this.apiUrl}/${id}`;
    return this.http.put<void>(url, product, {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    });
  }

  // Deleta um produto pelo ID
  deleteProduct(id: string): Observable<void> {
    const url = `${this.apiUrl}/${id}`;
    return this.http.delete<void>(url);
  }
}
