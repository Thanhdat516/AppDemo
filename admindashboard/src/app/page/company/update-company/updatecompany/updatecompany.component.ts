import { Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
// import { AppService } from 'src/app/services/app.service';
import { Company } from 'src/app/models/company';
import { AppService } from 'src/app/services/app.service';

import { Router } from '@angular/router';



@Component({
  selector: 'app-updatecompany',
  templateUrl: './updatecompany.component.html',
  styleUrls: ['./updatecompany.component.css'],
  // providers: [AppService]
})

export class UpdatecompanyComponent implements OnInit {
stringPattern = '^[^0-9]+$';
dataOld: any = {};
@Input() dataNew? : Company;
@Output() companyUpdated = new EventEmitter<Company[]>();
nameCompany: string = "";
phoneCompany: string = "";
addressCompany: string = "";
maskRules = {
  H: /^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$/
}

constructor(private appService: AppService, private router: Router) {}

  ngOnInit(): void {
    this.dataOld = this.appService.getdataCompany();
    this.nameCompany = this.dataOld.nameCompany;
    this.phoneCompany = this.dataOld.phoneCompany;
    this.addressCompany = this.dataOld.addressCompany;
  }

  updateCompany() {
    this.dataNew = {
      companyID: this.dataOld.companyID,
      nameCompany: this.nameCompany,
      phoneCompany: this.phoneCompany,
      addressCompany: this.addressCompany
    }

    this.appService.updateCompany(this.dataNew).subscribe((company: Company[]) => this.companyUpdated.emit(company));
    this.router.navigate(["company"]);
    alert("Cập nhật thành công");
  }
}
