import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private baseUrl = 'http://localhost:5296/api';

  private customersSubject = new BehaviorSubject<any[]>([]);
  customers$ = this.customersSubject.asObservable();

  private carsSubject = new BehaviorSubject<any[]>([]);
  cars$ = this.carsSubject.asObservable();

  constructor(private http: HttpClient) {
    this.loadCustomers();
    this.loadCars();
  }

  // Customers
  loadCustomers() {
    this.getCustomers().subscribe(data => this.customersSubject.next(data));
  }

  getCustomers(): Observable<any> {
    return this.http.get(`${this.baseUrl}/customers`);
  }

  getCustomer(id: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/customers/${id}`);
  }

  createCustomer(customer: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/customers`, customer);
  }

  updateCustomer(id: number, customer: any): Observable<any> {
    return this.http.put(`${this.baseUrl}/customers/${id}`, customer);
  }

  deleteCustomer(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/customers/${id}`);
  }

  // Cars
  loadCars() {
    this.getCars().subscribe(data => this.carsSubject.next(data));
  }

  getCars(): Observable<any> {
    return this.http.get(`${this.baseUrl}/cars`);
  }

  getCar(id: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/cars/${id}`);
  }

  createCar(car: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/cars`, car);
  }

  updateCar(id: number, car: any): Observable<any> {
    return this.http.put(`${this.baseUrl}/cars/${id}`, car);
  }

  deleteCar(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/cars/${id}`);
  }

  // Repairs
  getRepairs(): Observable<any> {
    return this.http.get(`${this.baseUrl}/repairs`);
  }

  getRepair(id: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/repairs/${id}`);
  }

  createRepair(repair: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/repairs`, repair);
  }

  updateRepair(id: number, repair: any): Observable<any> {
    return this.http.put(`${this.baseUrl}/repairs/${id}`, repair);
  }

  deleteRepair(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/repairs/${id}`);
  }

  uploadRepairFile(id: number, file: File): Observable<any> {
    const formData: FormData = new FormData();
    formData.append('file', file, file.name);
    return this.http.post(`${this.baseUrl}/repairs/${id}/upload`, formData);
  }
}
