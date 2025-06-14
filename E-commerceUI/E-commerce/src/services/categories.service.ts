import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment/environment.component';

export interface Category {
  categoryId: number;
  name: string;
  description: string;
}

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  private apiUrl = `${environment.apiUrl}/Category`;

  constructor(private http: HttpClient) { }

  // Get all categories
  getAllCategories(): Observable<Category[]> {
    return this.http.get<Category[]>(`${this.apiUrl}`);
  }

  // Get category by ID
  getCategoryById(id: number): Observable<Category> {
    return this.http.get<Category>(`${this.apiUrl}/${id}`);
  }

  // Add a new category
  addCategory(category: { name: string, description: string }): Observable<any> {
    return this.http.post(`${this.apiUrl}`, category);
  }

  // Update an existing category
  updateCategory(id: number, category: { name: string, description: string }): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, category);
  }

  // Delete a category
  deleteCategory(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
