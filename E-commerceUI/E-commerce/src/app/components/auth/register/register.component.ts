import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../../../services/auth-service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
@Component({
  selector: 'app-register',
  imports: [FormsModule, CommonModule]
,
  standalone: true,
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent {
  email: string = '';
  password: string = '';
  name: string = '';
  role: string = ''; // Default role

  constructor(private authService: AuthService, private router: Router) {}

  register(): void {
    this.authService.register(this.name, this.password, this.role, this.email).subscribe({
      next: (response) => {
        console.log('Registration success response:', response);
        alert('Registration successful! You can now log in.');
        this.router.navigate(['/login']);
      },
      error: (error) => {
        console.error('Registration error:', error);
        alert('Registration failed. Please try again.');
      }
    });
    
  }

}
