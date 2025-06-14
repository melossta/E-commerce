import { Component } from '@angular/core';
import { provideRouter, RouterOutlet } from '@angular/router';

import { LoginComponent } from './components/auth/login/login.component';
import { RegisterComponent } from './components/auth/register/register.component';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet,FormsModule],
  template: `<router-outlet></router-outlet>`,
  styleUrls: ['./app.scss']
})
export class App {}
