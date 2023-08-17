import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { DxDrawerModule, DxListModule, DxDataGridModule, DxButtonModule, DxFormModule, DxTextBoxModule, DxValidatorModule, DxSelectBoxModule } from 'devextreme-angular';

import { FormsModule } from '@angular/forms';

import { NavigationHeaderComponent } from './component/navigation-header/navigation-header.component';
import { BodyComponent } from './component/body/body.component';
import { CompanyComponent } from './component/company/company.component';
import { EmployeeComponent } from './component/employee/employee.component';
import { UpdatecompanyComponent } from './page/company/update-company/updatecompany/updatecompany.component';
import { AddcompanyComponent } from './page/company/addcompany/addcompany.component';
import { AddemployeeComponent } from './page/employee/addemployee/addemployee/addemployee.component';
import { UpdateemployeeComponent } from './page/employee/updateemployee/updateemployee.component';


@NgModule({
  declarations: [
    AppComponent,
    NavigationHeaderComponent,
    BodyComponent,
    CompanyComponent,
    EmployeeComponent,
    UpdatecompanyComponent,
    AddcompanyComponent,
    AddemployeeComponent,
    UpdateemployeeComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    DxDrawerModule,
    DxListModule,
    DxDataGridModule,
    DxButtonModule,
    DxFormModule,
    HttpClientModule,
    FormsModule,
    DxTextBoxModule,
    DxValidatorModule,
    DxSelectBoxModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
