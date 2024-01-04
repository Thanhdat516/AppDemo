import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import {
  DxDrawerModule,
  DxListModule,
  DxDataGridModule,
  DxButtonModule,
  DxFormModule,
  DxTextBoxModule,
  DxValidatorModule,
  DxSelectBoxModule,
} from 'devextreme-angular';
import { FormsModule } from '@angular/forms';
import { NavigationHeaderComponent } from './component/navigation-header/navigation-header.component';
import { BodyComponent } from './component/body/body.component';
import { CompanyComponent } from './component/company/company.component';
import { EmployeeComponent } from './component/employee/employee.component';
import { UpdatecompanyComponent } from './page/company/update-company/updatecompany/updatecompany.component';
import { AddcompanyComponent } from './page/company/addcompany/addcompany.component';
import { AddemployeeComponent } from './page/employee/addemployee/addemployee/addemployee.component';
import { UpdateemployeeComponent } from './page/employee/updateemployee/updateemployee.component';
import { LoginComponent } from './component/login/login.component';
import { AccessdeniedComponent } from './page/access-denied/accessdenied/accessdenied.component';
import { AuthInterceptorService } from './services/auth-interceptor.service';
import { TokenInterceptor } from './component/interceptor/token.interceptor';
import { NgxSpinnerModule } from 'ngx-spinner';
import { LoadingInterceptor } from './component/interceptor/loading-interceptor.interceptor';

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
    LoginComponent,
    AccessdeniedComponent,
  ],
  imports: [
    NgxSpinnerModule.forRoot({
      type: 'ball-pulse-sync',
    }),
    BrowserAnimationsModule,
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
    BrowserAnimationsModule,
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true,
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptorService,
      multi: true,
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: LoadingInterceptor,
      multi: true,
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
