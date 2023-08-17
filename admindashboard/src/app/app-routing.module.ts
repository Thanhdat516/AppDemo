import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CompanyComponent } from './component/company/company.component';
import { EmployeeComponent } from './component/employee/employee.component';
import { UpdatecompanyComponent } from './page/company/update-company/updatecompany/updatecompany.component';
import { AddcompanyComponent } from './page/company/addcompany/addcompany.component';
import { AddemployeeComponent } from './page/employee/addemployee/addemployee/addemployee.component';
import { UpdateemployeeComponent } from './page/employee/updateemployee/updateemployee.component';

const routes: Routes = [
  { path: '', redirectTo: '/company', pathMatch: 'full' },
  { path: 'company', component: CompanyComponent },
  { path: 'employee', component: EmployeeComponent },
  { path: 'addcompany', component: AddcompanyComponent },
  { path: 'updatecompany', component: UpdatecompanyComponent },
  { path: 'addemployee', component: AddemployeeComponent },
  { path: 'updateemployee', component: UpdateemployeeComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  declarations: [
    // CompanyComponent,
    // EmployeeComponent,
  ]
})
export class AppRoutingModule {
}
