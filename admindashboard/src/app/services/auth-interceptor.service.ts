import { Injectable } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import {catchError} from 'rxjs/operators';
import {Router} from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthInterceptorService implements HttpInterceptor{

  constructor(private router: Router, private authService: AuthService ) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const modifiedRequest = request.clone({
    });
    return next.handle(modifiedRequest)
    .pipe(
        catchError((error) => 
        {
          if ([401, 403].includes(error.status)) {
            this.authService.logOut();
            return throwError(()=>'Unauthorized');
          };
          return throwError(() => 'An error occurred');
      })
    );
  }
}
