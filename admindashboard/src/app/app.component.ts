import { Component } from '@angular/core';
import { filter } from 'rxjs/operators'
import { NavigationEnd, Router  } from '@angular/router';
import { AuthService } from './services/auth.service';
import { JwtHelperService } from '@auth0/angular-jwt';

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

  constructor(private router: Router, private authService: AuthService) {
    this.router.events
    .pipe(filter((rs): rs is NavigationEnd => rs instanceof NavigationEnd))
    .subscribe(event => {
      if (event.id === 1 && event.url === event.urlAfterRedirects) 
      {  
        this.token_current = this.authService.getToken();
        if(this.token_current) 
        {
          this.date = Math.floor((new Date().getTime() / 1000));
          this.decodedToken = this.jwtHelperService.decodeToken(this.token_current);
          this.authService.autoLogout((this.decodedToken.exp - this.date)*1000);
        }
      }
    })
  }
}
