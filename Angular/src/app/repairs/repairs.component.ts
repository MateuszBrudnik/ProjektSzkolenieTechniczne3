import { Component, OnInit } from '@angular/core';
import { ApiService } from '../services/api.service';
import { MatDialog } from '@angular/material/dialog';
import { EditRepairDialogComponent } from './edit-repair-dialog/edit-repair-dialog.component';

@Component({
  selector: 'app-repairs',
  templateUrl: './repairs.component.html',
  styleUrls: ['./repairs.component.css']
})
export class RepairsComponent implements OnInit {
  displayedColumns: string[] = ['description', 'date', 'car', 'actions'];
  repairs: any[] = [];
  cars: any[] = [];

  constructor(private apiService: ApiService, public dialog: MatDialog) { }

  ngOnInit(): void {
    this.loadRepairs();
    this.apiService.cars$.subscribe(data => {
      this.cars = data;
    });
  }

  loadRepairs() {
    this.apiService.getRepairs().subscribe(data => {
      this.repairs = data;
    });
  }

  addRepair(description: string, date: string, carId: string) {
    const newRepair = { description, date, carId: parseInt(carId, 10) };
    this.apiService.createRepair(newRepair).subscribe(() => {
      this.loadRepairs();
    });
  }

  deleteRepair(id: number) {
    this.apiService.deleteRepair(id).subscribe(() => {
      this.loadRepairs();
    });
  }

  editRepair(repair: any) {
    const dialogRef = this.dialog.open(EditRepairDialogComponent, {
      width: '250px',
      data: { ...repair, cars: this.cars }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.apiService.updateRepair(result.id, result).subscribe(() => {
          this.loadRepairs();
        });
      }
    });
  }
}
