import { Injectable } from '@angular/core';
// import { JwtHelperService  } from '@auth0/angular-jwt'
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from 'src/app/services/auth.service';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {

  constructor(private authSevice: AuthService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    const myToken = this.authSevice.getToken();
    if(myToken) {
      const authRequest = request.clone({
        setHeaders: {'Authorization': `Bearer ${myToken} `},
        withCredentials: true
      });
      return next.handle(authRequest);
    }
    const newRequest = request.clone({
      withCredentials: true
    });
    return next.handle(newRequest);
  }
}
