import { Component, OnInit } from '@angular/core';
import { User } from '../../models/user';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AlertifyService } from '../../../alertify.service';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  dataFormLogon: User = {};
  passwordButton: any;
  Username: string = '';
  Password: string = '';
  valueUser?: User;
  expire_date_token: any;
  date: number = 0;
  decodedToken: any;
  jwtHelperService = new JwtHelperService();
  ngOnInit(): void {
    if (this.authService.isLoggedIn() == true) {
      this.router.navigate(['company']);
    }
  }

  constructor(
    private router: Router,
    private authService: AuthService,
    private alertify: AlertifyService
  ) {}

  submitButtonOptions = {
    useSubmitBehavior: true,
  };

  handleSubmit() {
    this.valueUser = {
      username: this.Username,
      password: this.Password,
    };
    // this.authService
    //   .AuthenticationUser(this.valueUser)
    //   .subscribe((result: Response) => {
    //     if (result.success && result.data.accessToken) {
    //       this.date = Math.floor(new Date().getTime() / 1000);
    //       this.authService.storeToken(result.data.accessToken);
    //       this.decodedToken = this.jwtHelperService.decodeToken(
    //         result.data.accessToken
    //       );
    //       this.authService.autoLogout(
    //         (this.decodedToken.exp - this.date) * 1000
    //       );
    //       this.alertify.success('Login successfully');
    //       this.router.navigate(['company']);
    //     } else {
    //       this.alertify.error('Invalid user/password');
    //     }
    //   });
    this.authService.AuthenticationUser(this.valueUser).subscribe({
      next: (result) => {
        this.date = Math.floor(new Date().getTime() / 1000);
        this.authService.storeToken(result.data.accessToken);
        this.decodedToken = this.jwtHelperService.decodeToken(
          result.data.accessToken
        );
        this.authService.autoLogout((this.decodedToken.exp - this.date) * 1000);
        this.alertify.success('Login successfully');
        this.router.navigate(['company']);
      },
      error: () => {
        this.alertify.error('Invalid user/password');
      },
    });
  }
}
