import { Component } from '@angular/core';
import { CategoryService, Category } from '../../services/categories.service';
import { AuthService } from '../../services/auth-service';
import { OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
@Component({
  selector: 'app-categories.component',
  imports: [FormsModule, CommonModule],
  templateUrl: './categories.component.html',
  styleUrl: './categories.component.scss'
})
export class CategoryComponent implements OnInit {
  categories: any[] = [];
  name: string = '';
  description: string = '';
  editingCategoryId: number | null = null;
  

  constructor(private categoryService: CategoryService,private authService: AuthService) {}

  ngOnInit(): void {
    this.loadCategories();
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

  addCategory() {
    if (!this.name.trim()) {
      alert('Name is required.');
      return;
    }

    const newCategory = { name: this.name, description: this.description };

    this.categoryService.addCategory(newCategory).subscribe({
      next: () => {
        alert('Category added successfully.');
        this.resetForm();
        this.loadCategories();
      },
      error: (err) => {
        alert('Error adding category.');
        console.error(err);
      }
    });
  }

  deleteCategory(id: number) {
    if (confirm('Are you sure you want to delete this category?')) {
      this.categoryService.deleteCategory(id).subscribe({
        next: () => {
          alert('Category deleted successfully.');
          this.loadCategories();
        },
        error: (err) => {
          alert('Error deleting category.');
          console.error(err);
        }
      });
    }
  }

  startEdit(category: any) {
    this.editingCategoryId = category.categoryId;
    this.name = category.name;
    this.description = category.description;
  }

  updateCategory() {
    if (!this.editingCategoryId) return;

    const updatedCategory = { name: this.name, description: this.description };

    this.categoryService.updateCategory(this.editingCategoryId, updatedCategory).subscribe({
      next: () => {
        alert('Category updated successfully.');
        this.resetForm();
        this.loadCategories();
      },
      error: (err) => {
        alert('Error updating category.');
        console.error(err);
      }
    });
  }

  resetForm() {
    this.name = '';
    this.description = '';
    this.editingCategoryId = null;
  }
  logout() {
    this.authService.logout();
  }
  goToProducts() {
    window.location.href = '/products'; // Redirect to products page
  }
  goToShippingDetails() {
    window.location.href = '/shipping-details'; // Redirect to shipping details page
  }
}