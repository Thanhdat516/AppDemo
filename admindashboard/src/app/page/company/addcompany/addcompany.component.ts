import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AppService } from 'src/app/services/app.service';
import { Company } from 'src/app/models/company';
import { AlertifyService } from 'src/alertify.service';


@Component({
  selector: 'app-addcompany',
  templateUrl: './addcompany.component.html',
  styleUrls: ['./addcompany.component.css'],
})
export class AddcompanyComponent implements OnInit {
  // stringPattern = '^[^0-9]+$';
  @Input() valueCompany? : Company;
  @Output() companyUpdated = new EventEmitter<Company[]>();
  nameCompany: string = "";
  phoneCompany: string = "";
  addressCompany: string = "";

  maskRules = {
    H: /^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$/
  }
  
  constructor(private AppService: AppService, private alertify: AlertifyService) {
  }
  
  ngOnInit(): void {
  }
  
  onFormSubmit() {
    this.valueCompany = {
      nameCompany: this.nameCompany,
      phoneCompany: this.phoneCompany,
      addressCompany: this.addressCompany
    }
    this.AppService.createCompany(this.valueCompany).subscribe((companies: Company[]) => this.companyUpdated.emit(companies));
    window.location.reload()
    this.alertify.success("Created successful");
  }
}
