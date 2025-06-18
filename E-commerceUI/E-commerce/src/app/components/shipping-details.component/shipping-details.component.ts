import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { ShippingDetailsService, ShippingDetails } from '../../services/shipping-details.service';
import { AuthService } from '../../services/auth-service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatTableModule, MatTableDataSource } from '@angular/material/table';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';

@Component({
  selector: 'app-shipping-details',
  imports: [FormsModule, CommonModule, MatCardModule, MatFormFieldModule, MatInputModule, MatTableModule, MatPaginatorModule],
  templateUrl: './shipping-details.component.html',
  styleUrl: './shipping-details.component.scss'
})
export class ShippingDetailsComponent implements OnInit, AfterViewInit {
  shippingDetails: ShippingDetails[] = [];
  userId: number | null = null;

  // Form fields
  shippingDetailsId: number | null = null;
  address: string = '';
  city: string = '';
  postalCode: string = '';
  country: string = '';
  phoneNumber: string = '';

  displayedColumns = ['address', 'city', 'postalCode', 'country', 'phoneNumber', 'actions'];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  isEditModalOpen = false;

  constructor(private shippingDetailsService: ShippingDetailsService, private authService: AuthService) {}

  ngOnInit(): void {
    this.loadAllShippingDetails();
  }

  loadAllShippingDetails() {
    this.shippingDetailsService.getAllShippingDetails().subscribe({
      next: (data) => {
        this.dataSource.data = data;
        this.shippingDetails = data;
        this.dataSource.paginator = this.paginator;
      },
      error: (err) => {
        console.error('Error loading shipping details', err);
      }
    });
  }

  loadShippingDetailsByUserId() {
    if (this.userId === null) {
      alert('Please enter a user ID.');
      return;
    }

    this.shippingDetailsService.getShippingDetailsByUserId(this.userId).subscribe({
      next: (data) => {
        this.dataSource.data = data;
      },
      error: (err) => {
        alert('Error loading shipping details for the user.');
        console.error(err);
      }
    });
  }

    loadShippingDetailsByShippingId() {
    if (this.shippingDetailsId === null) {
      alert('Please enter a Shipping ID.');
      return;
    }

    this.shippingDetailsService.getShippingDetailById(this.shippingDetailsId).subscribe({
      next: (data) => {
        this.dataSource.data = [data];
      },
      error: (err) => {
        alert('Error loading shipping details for the Shipping ID.');
        console.error(err);
      }
    });
  }

  addShippingDetail() {
    const newShippingDetail = {
      userId: this.userId!,
      address: this.address,
      city: this.city,
      postalCode: this.postalCode,
      country: this.country,
      phoneNumber: this.phoneNumber
    };

    this.shippingDetailsService.addShippingDetail(newShippingDetail).subscribe({
      next: () => {
        alert('Shipping detail added successfully.');
        this.resetForm();
        this.loadAllShippingDetails();
      },
      error: (err) => {
        alert('Error adding shipping detail.');
        console.error(err);
      }
    });
  }

  deleteShippingDetail(id: number) {
    if (confirm('Are you sure you want to delete this shipping detail?')) {
      this.shippingDetailsService.deleteShippingDetail(id).subscribe({
        next: () => {
          alert('Shipping detail deleted successfully.');
          this.loadAllShippingDetails();
        },
        error: (err) => {
          alert('Error deleting shipping detail.');
          console.error(err);
        }
      });
    }
  }

  openEditModal(shippingDetail: ShippingDetails) {
    this.shippingDetailsId = shippingDetail.shippingDetailsId;
    this.userId = shippingDetail.userId;
    this.address = shippingDetail.address;
    this.city = shippingDetail.city;
    this.postalCode = shippingDetail.postalCode;
    this.country = shippingDetail.country;
    this.phoneNumber = shippingDetail.phoneNumber;

    this.isEditModalOpen = true;
  }

  closeEditModal() {
    this.isEditModalOpen = false;
    this.resetForm();
    this.shippingDetailsId = null;
  }

  updateShippingDetail() {
    if (this.shippingDetailsId === null) return;

    const updatedShippingDetail = {
      userId: this.userId!,
      address: this.address,
      city: this.city,
      postalCode: this.postalCode,
      country: this.country,
      phoneNumber: this.phoneNumber
    };

    this.shippingDetailsService.updateShippingDetail(this.shippingDetailsId, updatedShippingDetail).subscribe({
      next: () => {
        alert('Shipping detail updated successfully.');
        this.loadAllShippingDetails();
        this.closeEditModal();
      },
      error: (err) => {
        alert('Error updating shipping detail.');
        console.error(err);
      }
    });
  }

  resetForm() {
    this.address = '';
    this.city = '';
    this.postalCode = '';
    this.country = '';
    this.phoneNumber = '';
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }

  logout() {
    this.authService.logout();
  }
  
}
