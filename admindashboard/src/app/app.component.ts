import { Component } from '@angular/core';
import { catchError, filter } from 'rxjs/operators'
import { NavigationEnd, Router } from '@angular/router';
import { AuthService } from './services/auth.service';
import { JwtHelperService } from '@auth0/angular-jwt';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'admindashboard';
  date: number = 0;
  jwtHelperService = new JwtHelperService();
  decodedToken: any;
  token_current: any;
  test: any;

  constructor(private router: Router, private authService: AuthService, private http: HttpClient) {
    this.router.events
      .pipe(filter((rs): rs is NavigationEnd => rs instanceof NavigationEnd))
      .subscribe(event => {
        if (event.id === 1 && event.url === event.urlAfterRedirects) {
          //Timeout của Logout khi người dùng gửi request khác
          this.token_current = this.authService.getToken();
          if (this.token_current) {
            this.date = Math.floor((new Date().getTime() / 1000));
            this.decodedToken = this.jwtHelperService.decodeToken(this.token_current);
            this.authService.autoLogout((this.decodedToken.exp - this.date) * 1000);
          }
        }
      })
  }
}

