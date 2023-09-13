import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AppService } from 'src/app/services/app.service';

import { Router } from '@angular/router';

import { Employee } from 'src/app/models/employee';
import { AlertifyService } from 'src/alertify.service';
import { Company } from 'src/app/models/company';

@Component({
  selector: 'app-updateemployee',
  templateUrl: './updateemployee.component.html',
  styleUrls: ['../../company/update-company/updatecompany/updatecompany.component.css']
})
export class UpdateemployeeComponent implements OnInit{
dataOld: any = {};
@Input() dataNew? : Employee;
@Output() employeeUpdated = new EventEmitter<Employee[]>();
companyID: any;
nameEmployee: any = "";
phoneEmployee: string = "";
addressEmployee: string = "";
nameCompany: string = "";
maskRules = {
    H: /^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$/
  }

constructor(private appService: AppService, private router: Router, private alertify: AlertifyService) {}

  ngOnInit(): void {
    this.dataOld = this.appService.getdataEmployee();
    this.nameEmployee = this.dataOld.nameEmployee;
    this.phoneEmployee = this.dataOld.phoneEmployee;
    this.addressEmployee = this.dataOld.addressEmployee;
    this.nameCompany = this.dataOld.nameCompany;
    this.companyID = this.dataOld.companyID;
  }

  updateEmployee() {
    this.dataNew = {
      employeeID: this.dataOld.employeeID,
      nameEmployee: this.nameEmployee,
      phoneEmployee: this.phoneEmployee,
      addressEmployee: this.addressEmployee,
      companyID : this.companyID
    }
    this.appService.updateEmployee(this.dataNew).subscribe((employee: Employee[]) => this.employeeUpdated.emit(employee));
    this.router.navigate(["employee"]);
    this.alertify.success("Updated successful")
  }
}
