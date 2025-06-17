import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment/environment.component';

export interface ShippingDetails {
  shippingDetailsId: number;
  userId: number;
  userName?: string;
  address: string;
  city: string;
  postalCode: string;
  country: string;
  phoneNumber: string;
}

@Injectable({
  providedIn: 'root'
})
export class ShippingDetailsService {
  private apiUrl = `${environment.apiUrl}/ShippingDetails`;

  constructor(private http: HttpClient) { }

  // ✅ Get all shipping details
  getAllShippingDetails(): Observable<ShippingDetails[]> {
    return this.http.get<ShippingDetails[]>(`${this.apiUrl}`);
  }

  // ✅ Get all shipping details for a specific user
  getShippingDetailsByUserId(userId: number): Observable<ShippingDetails[]> {
    return this.http.get<ShippingDetails[]>(`${this.apiUrl}/by-user/${userId}`);
  }

  // ✅ Get shipping detail by ID
  getShippingDetailById(id: number): Observable<ShippingDetails> {
    return this.http.get<ShippingDetails>(`${this.apiUrl}/${id}`);
  }

  // might need to change the omit
  addShippingDetail(shippingDetail: Omit<ShippingDetails, 'shippingDetailsId' | 'userName'>): Observable<any> {
    return this.http.post(`${this.apiUrl}`, shippingDetail);
  }

  // might need to change the omit
  updateShippingDetail(id: number, shippingDetail: Omit<ShippingDetails, 'shippingDetailsId' | 'userName'>): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, shippingDetail);
  }

  // ✅ Delete shipping detail
  deleteShippingDetail(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
