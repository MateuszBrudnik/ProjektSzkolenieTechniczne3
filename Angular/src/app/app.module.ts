import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule } from '@angular/forms';

import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatTableModule } from '@angular/material/table';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatDialogModule } from '@angular/material/dialog';
import { MatTabsModule } from '@angular/material/tabs';

import { AppComponent } from './app.component';
import { UploadComponent } from './upload/upload.component';
import { CustomersComponent } from './customers/customers.component';
import { CarsComponent } from './cars/cars.component';
import { RepairsComponent } from './repairs/repairs.component';
import { EditCustomerDialogComponent } from './customers/edit-customer-dialog/edit-customer-dialog.component';
import { EditCarDialogComponent } from './cars/edit-car-dialog/edit-car-dialog.component';
import { EditRepairDialogComponent } from './repairs/edit-repair-dialog/edit-repair-dialog.component';
import { UploadDialogComponent } from './upload-dialog/upload-dialog.component';

@NgModule({
  declarations: [
    AppComponent,
    UploadComponent,
    CustomersComponent,
    CarsComponent,
    RepairsComponent,
    EditCustomerDialogComponent,
    EditCarDialogComponent,
    EditRepairDialogComponent,
    UploadDialogComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule,
    MatToolbarModule,
    MatButtonModule,
    MatIconModule,
    MatInputModule,
    MatSelectModule,
    MatTableModule,
    MatCardModule,
    MatFormFieldModule,
    MatDialogModule,
    MatTabsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
