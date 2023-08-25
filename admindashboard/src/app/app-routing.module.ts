import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CompanyComponent } from './component/company/company.component';
import { EmployeeComponent } from './component/employee/employee.component';
import { UpdatecompanyComponent } from './page/company/update-company/updatecompany/updatecompany.component';
import { AddcompanyComponent } from './page/company/addcompany/addcompany.component';
import { AddemployeeComponent } from './page/employee/addemployee/addemployee/addemployee.component';
import { UpdateemployeeComponent } from './page/employee/updateemployee/updateemployee.component';
import { LoginComponent } from './component/login/login.component';
import { BodyComponent } from './component/body/body.component';
import { AccessdeniedComponent } from './page/access-denied/accessdenied/accessdenied.component';
import { authGuard } from './guards/auth.guard';

const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  {
    path: '',
    component: BodyComponent,
    children: [
      { path: 'company', component: CompanyComponent, canActivate: [authGuard] },
      { path: 'employee', component: EmployeeComponent, canActivate: [authGuard]  },
      { path: 'addcompany', component: AddcompanyComponent, canActivate: [authGuard]  },
      { path: 'updatecompany', component: UpdatecompanyComponent, canActivate: [authGuard]},
      { path: 'addemployee', component: AddemployeeComponent, canActivate: [authGuard]  },
      { path: 'updateemployee', component: UpdateemployeeComponent, canActivate: [authGuard]},
    ],
  },
  { path: 'accessdenid', component: AccessdeniedComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  declarations: [],
})
export class AppRoutingModule {}
