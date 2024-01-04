import { Injectable } from '@angular/core';
import { delay } from 'rxjs/operators';
import { AuthService } from 'src/app/services/auth.service';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AlertifyService } from '../../alertify.service';

@Injectable({
  providedIn: 'root',
})
export class AuthInterceptorService implements HttpInterceptor {
  constructor(
    private authService: AuthService,
    private alertify: AlertifyService
  ) {}

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    const modifiedRequest = request.clone({});
    return next.handle(modifiedRequest).pipe(
      catchError((error) => {
        if ([401, 403].includes(error.status)) {
          this.authService.logOut();
          return throwError(() => 'Unauthorized');
        }
        return throwError(() => 'An error occurred');
      })
    );
  }
}
