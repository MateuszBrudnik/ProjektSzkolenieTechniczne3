import { Component, OnInit } from '@angular/core';
import { ApiService } from '../services/api.service';
import { MatDialog } from '@angular/material/dialog';
import { UploadDialogComponent } from '../upload-dialog/upload-dialog.component';
import {EditRepairDialogComponent} from "./edit-repair-dialog/edit-repair-dialog.component";

@Component({
  selector: 'app-repairs',
  templateUrl: './repairs.component.html',
  styleUrls: ['./repairs.component.css']
})
export class RepairsComponent implements OnInit {
  repairs: any[] = [];
  cars: any[] = [];
  displayedColumns: string[] = ['description', 'date', 'car', 'file', 'actions'];

  constructor(private apiService: ApiService, public dialog: MatDialog) {}

  ngOnInit(): void {
    this.loadRepairs();
    this.loadCars();
  }

  loadRepairs(): void {
    this.apiService.getRepairs().subscribe(data => {
      this.repairs = data;
    });
  }

  loadCars(): void {
    this.apiService.getCars().subscribe(data => {
      this.cars = data;
    });
  }

  addRepair(description: string, date: string, carId: number): void {
    const newRepair = { description, date, carId };
    this.apiService.createRepair(newRepair).subscribe(() => {
      this.loadRepairs();
    });
  }

  deleteRepair(id: number): void {
    this.apiService.deleteRepair(id).subscribe(() => {
      this.loadRepairs();
    });
  }

  editRepair(repair: any): void {
    const dialogRef = this.dialog.open(EditRepairDialogComponent, {
      width: '400px',
      data: repair
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.apiService.updateRepair(result.id, result).subscribe(() => {
          this.loadRepairs();
        });
      }
    });
  }

  openUploadDialog(repairId: number): void {
    const dialogRef = this.dialog.open(UploadDialogComponent, {
      width: '400px',
      data: { repairId }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadRepairs();
      }
    });
  }
}
