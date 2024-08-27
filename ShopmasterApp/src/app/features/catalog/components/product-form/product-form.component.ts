import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CatalogService } from '../../services/catalog.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-product-form',
  templateUrl: './product-form.component.html',
  styleUrls: ['./product-form.component.css']
})
export class ProductFormComponent {
  productForm: FormGroup;
  selectedImage?: File;
  categories: { id: number, name: string }[] = [];


  ngOnInit(): void {
    // Exemplo de categorias, substitua pelos dados do seu banco de dados
    this.categories = [
      { id: 1, name: 'Electronics' },
      { id: 2, name: 'Clothing' },
      { id: 3, name: 'Books' },
      { id: 4, name: 'Home & Kitchen' }
    ];

    // Se precisar buscar categorias do servidor, use o serviço
    // this.catalogService.getCategories().subscribe(categories => this.categories = categories);
  }

  constructor(
    private fb: FormBuilder,
    private catalogService: CatalogService,
    private router: Router
  ) {
    this.productForm = this.fb.group({
      name: ['', [Validators.required]],
      price: ['', [Validators.required, Validators.min(0)]],
      description: [''],
      categoryId: ['', [Validators.required]],
      image: [null]
    });
  }

  onImageChange(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      this.selectedImage = input.files[0];
    }
  }

  onSubmit(): void {
    if (this.productForm.valid) {
      const newProduct = this.productForm.value;
      this.catalogService.createProduct(newProduct, this.selectedImage).subscribe({
        next: () => {
          this.router.navigate(['/home']);
        },
        error: (error) => {
          console.error('Error creating product', error);
        }
      });
    }
  }
}
