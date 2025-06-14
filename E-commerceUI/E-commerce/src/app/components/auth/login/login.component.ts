import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { AuthService } from '../../../../services/auth-service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  standalone: true,
  imports: [FormsModule, CommonModule]
})
export class LoginComponent {
  email: string = '';
  password: string = '';
  errorMessage: string = '';
  successMessage: string = '';
  constructor(private authService: AuthService, private router: Router, private route: ActivatedRoute) {}
  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      if (params['registered'] === 'true') {
        this.successMessage = 'Registration successful! Please log in.';
      }
    });
  }
  




  login(): void {
    this.authService.login(this.email, this.password).subscribe(
      (response) => {
        const token = response.token;
        const role = response.roles[0]; // Assuming the first role is the primary one
  
        // Save token and role to localStorage
        localStorage.setItem('authToken', token);
        localStorage.setItem('role', role);
  
        // Show success message
        // this.successMessage = `Successfully logged in as ${role}.`;
        // this.errorMessage = ''; // Clear error if any
        alert(`Successfully logged in as ${role}.`);
        this.router.navigate(['/categories']); // Redirect to home page or dashboard
      },
      (error) => {
        
        // this.errorMessage = 'Invalid email or password. Please try again.';
        alert('Invalid email or password. Please try again.')
      }
    );
  }
  logout() {
    this.successMessage = '';
    this.errorMessage = '';
    this.successMessage = 'Successfully logged out.';
    this.authService.logout();

  }
  goToRegister(): void {
    this.router.navigate(['/register']);
  }
  


}
