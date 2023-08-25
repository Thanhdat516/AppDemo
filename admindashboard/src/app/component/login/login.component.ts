import { Component, Input, OnInit } from '@angular/core';
import { User } from '../../models/user'
import { Router } from '@angular/router';
import { Response } from 'src/app/models/response';
import { AuthService } from 'src/app/services/auth.service';
import { JwtHelperService } from '@auth0/angular-jwt';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent implements OnInit {
    dataFormLogon: User = {};
    passwordButton: any;
    Username: string = "";
    Password: string = "";
    valueUser? : User;
    expire_date_token: any;
    date: number = 0;
    decodedToken: any;
    jwtHelperService = new JwtHelperService();
    ngOnInit(): void {
        if(this.authService.issLoggedIn() == true) {
            this.router.navigate(["company"]);
        }
    }

    constructor(private router: Router, private authService: AuthService) {
        
    }

    submitButtonOptions = {
        useSubmitBehavior: true
    }

    handleSubmit () {
        this.valueUser =  {
            username: this.Username,
            password: this.Password,
        }
        this.authService.AuthenticationUser(this.valueUser).subscribe((result: Response) =>
        {
            if(result.success && result.data) {
                this.date = Math.floor((new Date().getTime() / 1000));
                alert(result.message);
                this.router.navigate(["company"]);
                this.authService.storeToken(result.data);
                this.decodedToken = this.jwtHelperService.decodeToken(result.data);
                this.authService.autoLogout((this.decodedToken.exp - this.date)*1000);
            }
            else {
                alert(result.message);
            }
        });
    }
}
