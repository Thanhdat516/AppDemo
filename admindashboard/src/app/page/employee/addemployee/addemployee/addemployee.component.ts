import { Component, EventEmitter, Input, Output } from '@angular/core';
import { AppService } from 'src/app/services/app.service';
import { Employee } from 'src/app/models/employee';
import { Company } from 'src/app/models/company';

@Component({
  selector: 'app-addemployee',
  templateUrl: './addemployee.component.html',
  styleUrls: ['../../../company/addcompany/addcompany.component.css']
})
export class AddemployeeComponent {
  nameEmployee: string = "";
  phoneEmployee: string = "";
  addressEmployee: string = "";
  listCompany: Company[] = [];
  getCompanyID?: number;

  maskRules = {
    H: /^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$/
  }

  @Input() valueEmployee? : Employee;
  @Output() employeeUpdated = new EventEmitter<Employee[]>();
  
  constructor(private AppService: AppService) {
    this.AppService.GetCompanies().subscribe((result: Company[]) =>
    {
      this.listCompany = result;
    });
  }
  
  ngOnInit() { 
  }
  onValueChanged(e: any) {
    this.getCompanyID = e.value;
  }
  
  onFormSubmit() {
    this.valueEmployee = {
      nameEmployee: this.nameEmployee,
      phoneEmployee: this.phoneEmployee,
      addressEmployee: this.addressEmployee,
      companyID: this.getCompanyID,
    }
    this.AppService.createEmployee(this.valueEmployee).subscribe((employees: Employee[]) => this.employeeUpdated.emit(employees));
    window.location.reload()
    alert("Thêm thành công");
  }
}
