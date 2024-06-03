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
  displayedColumns: string[] = ['make', 'model', 'year', 'owner', 'actions'];
  cars: any[] = [];
  customers: any[] = [];

  constructor(private apiService: ApiService, public dialog: MatDialog) { }

  ngOnInit(): void {
    this.apiService.cars$.subscribe(data => {
      this.cars = data;
    });
    this.apiService.customers$.subscribe(data => {
      this.customers = data;
    });
  }

  addCar(make: string, model: string, year: string, customerId: string) {
    const newCar = { make, model, year: parseInt(year, 10), customerId: parseInt(customerId, 10) };
    this.apiService.createCar(newCar).subscribe(() => {
      this.apiService.loadCars();
    });
  }

  deleteCar(id: number) {
    this.apiService.deleteCar(id).subscribe(() => {
      this.apiService.loadCars();
    });
  }

  editCar(car: any) {
    const dialogRef = this.dialog.open(EditCarDialogComponent, {
      width: '250px',
      data: { ...car, customers: this.customers }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.apiService.updateCar(result.id, result).subscribe(() => {
          this.apiService.loadCars();
        });
      }
    });
  }
}
