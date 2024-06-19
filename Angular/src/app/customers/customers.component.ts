import { Component, OnInit } from '@angular/core';
import { ApiService } from '../services/api.service';
import { MatDialog } from '@angular/material/dialog';
import { EditCustomerDialogComponent } from './edit-customer-dialog/edit-customer-dialog.component';

@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.css']
})
export class CustomersComponent implements OnInit {
  customers: any[] = [];
  displayedColumns: string[] = ['name', 'email', 'actions'];

  constructor(private apiService: ApiService, public dialog: MatDialog) {}

  ngOnInit(): void {
    this.loadCustomers();
  }

  loadCustomers(): void {
    this.apiService.getCustomers().subscribe(data => {
      this.customers = data;
    });
  }

  addCustomer(name: string, email: string): void {
    const newCustomer = { name, email };
    this.apiService.createCustomer(newCustomer).subscribe(() => {
      this.loadCustomers();
    });
  }

  deleteCustomer(id: number): void {
    this.apiService.deleteCustomer(id).subscribe(() => {
      this.loadCustomers();
    });
  }

  editCustomer(customer: any): void {
    const dialogRef = this.dialog.open(EditCustomerDialogComponent, {
      width: '400px',
      data: customer
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.apiService.updateCustomer(result.id, result).subscribe(() => {
          this.loadCustomers();
        });
      }
    });
  }
}
