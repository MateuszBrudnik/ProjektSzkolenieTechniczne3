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
  displayedColumns: string[] = ['name', 'email', 'actions'];
  customers: any[] = [];

  constructor(private apiService: ApiService, public dialog: MatDialog) { }

  ngOnInit(): void {
    this.apiService.customers$.subscribe(data => {
      this.customers = data;
    });
  }

  addCustomer(name: string, email: string) {
    const newCustomer = { name, email };
    this.apiService.createCustomer(newCustomer).subscribe(() => {
      this.apiService.loadCustomers();
    });
  }

  deleteCustomer(id: number) {
    this.apiService.deleteCustomer(id).subscribe(() => {
      this.apiService.loadCustomers();
    });
  }

  editCustomer(customer: any) {
    const dialogRef = this.dialog.open(EditCustomerDialogComponent, {
      width: '250px',
      data: { ...customer }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.apiService.updateCustomer(result.id, result).subscribe(() => {
          this.apiService.loadCustomers();
        });
      }
    });
  }
}
