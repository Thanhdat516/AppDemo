import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AppService } from 'src/app/services/app.service';

import { Router } from '@angular/router';

import { Employee } from 'src/app/models/employee';

@Component({
  selector: 'app-updateemployee',
  templateUrl: './updateemployee.component.html',
  styleUrls: ['../../company/update-company/updatecompany/updatecompany.component.css']
})
export class UpdateemployeeComponent implements OnInit{
dataOld: any = {};
@Input() dataNew? : Employee;
@Output() employeeUpdated = new EventEmitter<Employee[]>();
nameEmployee: any = "";
phoneEmployee: string = "";
addressEmployee: string = "";
nameCompany: string = "";

constructor(private appService: AppService, private router: Router) {}

  ngOnInit(): void {
    this.dataOld = this.appService.getdataEmployee()
    this.nameEmployee = this.dataOld.nameEmployee;
    this.phoneEmployee = this.dataOld.phoneEmployee;
    this.addressEmployee = this.dataOld.addressEmployee;
    this.nameCompany = this.dataOld.nameCompany;
  }

  updateEmployee() {
    this.dataNew = {
      employeeID: this.dataOld.employeeID,
      nameEmployee: this.nameEmployee,
      phoneEmployee: this.phoneEmployee,
      addressEmployee: this.addressEmployee,
    }

    this.appService.updateEmployee(this.dataNew).subscribe((employee: Employee[]) => this.employeeUpdated.emit(employee));
    this.router.navigate(["employee"]);
    alert("Cập nhật thành công");
  }
}
