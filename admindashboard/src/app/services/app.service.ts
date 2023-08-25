import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';
import { environment } from 'src/environments/environment';
import { Company } from '../models/company';
import { Employee } from '../models/employee';

@Injectable({
  providedIn: 'root'
})

export class AppService {
  private urlCompany = "Company";
  private urlEmployee = "Employee";
  private dataCompany = {};
  private dataEmployee = {};
  constructor(private http: HttpClient) { }

  // Lấy dữ liệu cũ của công ty
  updatedataCompany(data: {}) {
    this.dataCompany = data;
    return this.dataCompany;
  }

  getdataCompany() {
    return this.dataCompany;
  }

  // Lấy dữ liệu cũ của employee
  updatedataEmployee(data: {}) {
    this.dataEmployee = data;
    return this.dataEmployee;
  }

  getdataEmployee() {
    return this.dataEmployee;
  }

  // Call api của company
  public GetCompanies(): Observable<any> {
    return this.http.get<any>(`${environment.apiUrl}/${this.urlCompany}`);
  }

  public createCompany(company: Company): Observable<Company[]> {
    return this.http.post<Company[]>(`${environment.apiUrl}/${this.urlCompany}`, company);
  }

  public updateCompany(company: Company): Observable<Company[]> {
    return this.http.put<Company[]>(`${environment.apiUrl}/${this.urlCompany}`, company);
  }

  public deleteCompany(company: Company): Observable<Company[]> {
    return this.http.delete<Company[]>(`${environment.apiUrl}/${this.urlCompany}/${company.companyID}`);
  }

  // Call api của employee
  public getEmployee(): Observable<any> {
    return this.http.get<any>(`${environment.apiUrl}/${this.urlEmployee}`);
  }

  public createEmployee(employee: Employee): Observable<Employee[]> {
    return this.http.post<Employee[]>(`${environment.apiUrl}/${this.urlEmployee}`, employee);
  }

  public updateEmployee(employee: Employee): Observable<Employee[]> {
    return this.http.put<Employee[]>(`${environment.apiUrl}/${this.urlEmployee}`, employee);
  }

  public deleteEmployee(employee: Employee): Observable<Employee[]> {
    return this.http.delete<Employee[]>(`${environment.apiUrl}/${this.urlEmployee}/${employee.employeeID}`);
  }
}
