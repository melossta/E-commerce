import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment/environment.component';

export interface Product {
  productId: number;
  name: string;
  description: string;
  price: number;
  stockQuantity: number;
  categoryId: number;

}

@Injectable({
  providedIn: 'root'
})
export class ProductsService {
  private apiUrl = `${environment.apiUrl}/Product`;

  constructor(private http: HttpClient) { }

  // Get all products
  getAllProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(`${this.apiUrl}`);
  }
  
  addProduct(product: { name: string, description: string, price: number, stockQuantity: number, categoryId: number }): Observable<any> {
    return this.http.post(`${this.apiUrl}`, product);
  }

  updateProduct(id: number, product: { name: string, description: string, price: number, stockQuantity: number, categoryId: number }): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, product);
  }

  deleteProduct(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
  getProductById(id: number): Observable<Product> {
    return this.http.get<Product>(`${this.apiUrl}/${id}`);
  }

}
