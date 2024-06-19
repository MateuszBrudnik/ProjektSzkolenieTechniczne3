import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private baseUrl = 'http://localhost:5296/api';

  constructor(private http: HttpClient) {}

  // Methods for Cars
  getCars(): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/cars`);
  }

  createCar(car: any): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}/cars`, car);
  }

  deleteCar(id: number): Observable<any> {
    return this.http.delete<any>(`${this.baseUrl}/cars/${id}`);
  }

  updateCar(id: number, car: any): Observable<any> {
    return this.http.put<any>(`${this.baseUrl}/cars/${id}`, car);
  }

  // Methods for Customers
  getCustomers(): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/customers`);
  }

  createCustomer(customer: any): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}/customers`, customer);
  }

  deleteCustomer(id: number): Observable<any> {
    return this.http.delete<any>(`${this.baseUrl}/customers/${id}`);
  }

  updateCustomer(id: number, customer: any): Observable<any> {
    return this.http.put<any>(`${this.baseUrl}/customers/${id}`, customer);
  }

  // Methods for Repairs
  getRepairs(): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/repairs`);
  }

  createRepair(repair: any): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}/repairs`, repair);
  }

  deleteRepair(id: number): Observable<any> {
    return this.http.delete<any>(`${this.baseUrl}/repairs/${id}`);
  }

  updateRepair(id: number, repair: any): Observable<any> {
    return this.http.put<any>(`${this.baseUrl}/repairs/${id}`, repair);
  }

  uploadRepairFile(repairId: number, file: File): Observable<any> {
    const formData = new FormData();
    formData.append('file', file);

    return this.http.post<any>(`${this.baseUrl}/repairs/${repairId}/upload`, formData);
  }
}
