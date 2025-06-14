import { Component } from '@angular/core';
import { ProductsService } from '../../../services/products.service';
import { CategoryService } from '../../../services/categories.service';
import { AuthService } from '../../../services/auth-service';
import { OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatTableModule } from '@angular/material/table'; // New Import
import { MatTableDataSource } from '@angular/material/table';
import { MatSelectModule } from '@angular/material/select';
import { ViewChild, AfterViewInit } from '@angular/core';
import { MatPaginator, MatPaginatorModule, PageEvent } from '@angular/material/paginator'; // New Import
@Component({
  selector: 'app-products.component',
  imports: [FormsModule, CommonModule, MatCardModule, MatFormFieldModule, MatInputModule, MatTableModule, MatPaginatorModule,MatSelectModule],
  templateUrl: './products.component.html',
  styleUrl: './products.component.scss'
})
export class ProductsComponent implements OnInit , AfterViewInit {
  products: any[] = [];
  name: string = '';
  description: string = '';
  price: number | null = null;
  stockQuantity: number | null = null;
  categoryId: number | null = null;
  editingProductId: number | null = null;
  categories: any[] = [];

   displayedColumns = ['name', 'description', 'price', 'stockQuantity', 'categoryId', 'actions'];
dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator) paginator!: MatPaginator;


  constructor(private productsService: ProductsService, private categoryService: CategoryService, private authService: AuthService) {}

  ngOnInit(): void {
    this.loadProducts();
    this.loadCategories(); 
  }

  // loadProducts() {
  //   this.productsService.getAllProducts().subscribe({
  //     next: (data) => {
  //       this.products = data;
  //     },
  //     error: (err) => {
  //       console.error('Error loading products', err);
  //     }
  //   });
  // }

loadProducts() {
  this.productsService.getAllProducts().subscribe({
    next: (data) => {
      this.dataSource.data = data;

      // Re-assign paginator after data is updated
      this.dataSource.paginator = this.paginator;
    },
    error: (err) => {
      console.error('Error loading products', err);
    }
  });
}
loadCategories() {
  this.categoryService.getAllCategories().subscribe({
    next: (data) => {
      this.categories = data;
    },
    error: (err) => {
      console.error('Error loading categories', err);
    }
  });
}



  addProduct() {
    if (!this.name.trim() || this.price === null || this.stockQuantity === null || this.categoryId === null) {
      alert('All fields are required.');
      return;
    }

    const newProduct = {
      name: this.name,
      description: this.description,
      price: this.price,
      stockQuantity: this.stockQuantity,
      categoryId: this.categoryId
    };

    this.productsService.addProduct(newProduct).subscribe({
      next: () => {
        alert('Product added successfully.');
        this.resetForm();
        this.loadProducts();
      },
      error: (err) => {
        alert('Error adding product.');
        console.error(err);
      }
    });
  }
  deleteProduct(id: number) {
    if (confirm('Are you sure you want to delete this product?')) {
      this.productsService.deleteProduct(id).subscribe({
        next: () => {
          alert('Product deleted successfully.');
          this.loadProducts();
        },
        error: (err) => {
          alert('Error deleting product.');
          console.error(err);
        }
      });
    }
  } 
  resetForm() {
    this.name = ''; 
    this.description = '';
    this.price = null;
    this.stockQuantity = null;
    this.categoryId = null;
  }
  startEdit(product: any) {
    this.editingProductId = product.productId;
    this.name = product.name;
    this.description = product.description;
    this.price = product.price;
    this.stockQuantity = product.stockQuantity;
    this.categoryId = product.categoryId;
  }
  updateProduct() {
    if (!this.editingProductId) return;

  const updatedProduct = {
    name: this.name,
    description: this.description,
    price: this.price!, // force tell TypeScript it's not null
    stockQuantity: this.stockQuantity!,
    categoryId: this.categoryId!
  };


    this.productsService.updateProduct(this.editingProductId, updatedProduct).subscribe({
      next: () => {
        alert('Product updated successfully.');
        this.loadProducts();
        this.closeEditModal();
      },
      error: (err) => {
        alert('Error updating product.');
        console.error(err);
      }
    });
  }
  cancelEdit() {
  this.editingProductId = null;
}
  isEditModalOpen = false;

openEditModal(product: any) {
  this.editingProductId = product.productId;
  this.name = product.name;
  this.description = product.description;
  this.price = product.price;
  this.stockQuantity = product.stockQuantity;
  this.categoryId = product.categoryId;
  this.isEditModalOpen = true;
}

closeEditModal() {
  this.isEditModalOpen = false;
  this.resetForm();
  this.editingProductId = null;
}
  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }


  logout() {
    this.authService.logout();
  }

//   //Helper method to get category name by ID
getCategoryName(categoryId: number): string {
  const category = this.categories.find(c => c.categoryId === categoryId);
  return category ? category.name : 'Unknown';
}


}
