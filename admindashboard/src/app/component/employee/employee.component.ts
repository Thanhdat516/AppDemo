import { Component, EventEmitter, Input, Output  } from '@angular/core';
import { Router } from '@angular/router';
import { Employee } from 'src/app/models/employee';
import { Company } from 'src/app/models/company';
import { AppService } from 'src/app/services/app.service';

@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html',
  styleUrls: ['./employee.component.css']
})
export class EmployeeComponent {
  employeeInfo: any[] = [];
  nameCompanys: Company = {};
  dataEmployee!: {};
  autoNavigateToFocusedRow = true;
  editEnabled= true;

  @Input() valueEmployee? : Employee;
  @Output() employeeUpdated = new EventEmitter<Employee[]>();

  constructor (private router: Router, private appService: AppService) {
  }
  
  ngOnInit(): void { 
    var count = 0;
    this.appService.getEmployee().subscribe((result: Employee[]) => (
      result.map(item_employee => {
        this.appService.GetCompanies().subscribe((result_nameCompany: Company[]) => (result_nameCompany.map(item_company => 
          {
            this.nameCompanys.companyID = item_company.companyID,
            this.nameCompanys.nameCompany = item_company.nameCompany
            if(item_company.companyID == item_employee.companyID)
            {
              this.employeeInfo.push(Object.assign(item_employee, this.nameCompanys));
            }
            var checkDuplicate = this.employeeInfo.find(x =>x.employeeID == item_employee.employeeID)
            if(item_employee.companyID == 0)
            {
              if(checkDuplicate == undefined)
              {
                this.employeeInfo.push(item_employee);
              }
            }
          }
        )))
      })
    ))
  }
  addEmployee(pageName: string):void {
    this.router.navigate([`${pageName}`])
  }

  updateEmployee(pageName: string, data: any) {
    this.router.navigate([`${pageName}`]);
    this.dataEmployee = this.appService.updatedataEmployee(data.data);
  }

  deleteEmployee(data: any) {
    this.valueEmployee = {
      employeeID: data.data.employeeID,
      nameEmployee: data.data.nameEmployee,
      phoneEmployee: data.data.phoneEmployee,
      addressEmployee: data.data.addressEmployee,
      companyID: data.data.companyID,
    }
    if(confirm("Are you sure to delete ")) {
      this.appService.deleteEmployee(this.valueEmployee).subscribe((employee: Employee[]) => this.employeeUpdated.emit(employee));
      window.location.reload();
    }
  }
}
