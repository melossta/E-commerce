import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment/environment.component';
import { Router } from '@angular/router';
@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = `${environment.apiUrl}/Auth`;

  constructor(private http: HttpClient, private router: Router) { }

  login(email: string, password: string): Observable<any> {
    const body = { email, password };
    return this.http.post(`${this.apiUrl}/login`, body);
  }
  register(name: string, password: string, role:string, email: string): Observable<any> {
    const body = { name, password, role, email };
    return this.http.post(`${this.apiUrl}/register`, body);
  }

  saveToken(token: string): void {
    localStorage.setItem('authToken', token);
  }
decodeToken(): any | null {
  const token = this.getToken();
  if (!token) return null;

  try {
    const payloadBase64 = token.split('.')[1];
    const payload = JSON.parse(atob(payloadBase64));

    if (payload.UserId) {
      localStorage.setItem('userId', payload.UserId);
    }

    return payload;
  } catch (error) {
    console.error('Error decoding token', error);
    return null;
  }
}

  getToken(): string | null {
    return localStorage.getItem('authToken');
  }

  logout(): void {
    localStorage.removeItem('authToken');
    localStorage.removeItem('role');
    this.router.navigate(['/login']); // redirect to login page after logout
  }
  
}
