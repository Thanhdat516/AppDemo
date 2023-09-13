import { HttpErrorResponse } from '@angular/common/http';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Router } from '@angular/router';
import { throwError } from 'rxjs';
import { Company } from 'src/app/models/company';
import { AppService } from 'src/app/services/app.service';
import { AlertifyService } from '../../../alertify.service'

@Component({
  selector: 'app-company',
  templateUrl: './company.component.html',
  styleUrls: ['./company.component.css'],
})
export class CompanyComponent {
  button: any;
  companyInfo: Company[] = [];
  datacompany!: {};
  autoNavigateToFocusedRow = true;
  editEnabled= true;
  error = null;
  
  @Input() valuecompany? : Company;
  @Output() companyUpdated = new EventEmitter<Company[]>();
  constructor (private router: Router, private appService: AppService, private alertijs:AlertifyService) {}
  
  ngOnInit(): void { 
    // this.appService.GetCompanies().subscribe((result: Company[]) => (this.companyInfo = result));
    this.appService.GetCompanies().subscribe(
      (result: any) => {
        this.companyInfo = result;
      }, 
    )
  }
  
  updateCompany(pageName: string, data: any) {
    this.router.navigate([`${pageName}`]);
    this.datacompany = this.appService.updatedataCompany(data.data);
  }

  addCompany(pageName: string) :void {
    this.router.navigate([`${pageName}`]);
  }

  deleteCompany(data: any) {
    this.valuecompany = {
      companyID: data.data.companyID,
      nameCompany: data.data.namecompany,
      phoneCompany: data.data.phone,
      addressCompany: data.data.address,
    }
    if(confirm("Are you sure to delete ")) {
      this.appService.deleteCompany(this.valuecompany).subscribe((companies: Company[]) => this.companyUpdated.emit(companies));
      window.location.reload();
    }
  }
}
