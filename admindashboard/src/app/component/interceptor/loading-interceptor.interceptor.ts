import { Injectable } from '@angular/core';
import { delay, finalize } from 'rxjs/operators';
import { NgxSpinnerService } from 'ngx-spinner';
import { tap } from 'rxjs/operators';

import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class LoadingInterceptor implements HttpInterceptor {
  constructor(private spinner: NgxSpinnerService) {}

  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    this.spinner.show(undefined, {
      type: 'ball-pulse-sync',
      bdColor: 'rgba(0,0,0,0.8)',
      color: '#fff',
      fullScreen: true,
      size: 'medium',
    });
    return next.handle(request).pipe(
      delay(1000),
      finalize(() => {
        this.spinner.hide();
      })
    );
  }
}
