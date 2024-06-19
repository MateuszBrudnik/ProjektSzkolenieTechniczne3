import { Component, OnInit } from '@angular/core';
import { ApiService } from '../services/api.service';
import { MatDialog } from '@angular/material/dialog';
import { EditCarDialogComponent } from './edit-car-dialog/edit-car-dialog.component';

@Component({
  selector: 'app-cars',
  templateUrl: './cars.component.html',
  styleUrls: ['./cars.component.css']
})
export class CarsComponent implements OnInit {
  cars: any[] = [];
  customers: any[] = [];
  displayedColumns: string[] = ['make', 'model', 'year', 'customer', 'actions'];

  constructor(private apiService: ApiService, public dialog: MatDialog) {}

  ngOnInit(): void {
    this.loadCars();
    this.loadCustomers();
  }

  loadCars(): void {
    this.apiService.getCars().subscribe(data => {
      this.cars = data;
    });
  }

  loadCustomers(): void {
    this.apiService.getCustomers().subscribe(data => {
      this.customers = data;
    });
  }

  addCar(make: string, model: string, year: string, customerId: string): void {
    const newCar = { make, model, year: parseInt(year), customerId: parseInt(customerId) };
    this.apiService.createCar(newCar).subscribe(() => {
      this.loadCars();
    });
  }

  deleteCar(id: number): void {
    this.apiService.deleteCar(id).subscribe(() => {
      this.loadCars();
    });
  }

  editCar(car: any): void {
    const dialogRef = this.dialog.open(EditCarDialogComponent, {
      width: '400px',
      data: car
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.apiService.updateCar(result.id, result).subscribe(() => {
          this.loadCars();
        });
      }
    });
  }
}
