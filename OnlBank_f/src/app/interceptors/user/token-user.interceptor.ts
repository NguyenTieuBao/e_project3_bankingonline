import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';
import { NgToastService } from 'ng-angular-popup';
import { Router } from '@angular/router';
import { ApiService } from '../../services/api.service';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {

  constructor(private api: ApiService, private toast: NgToastService, private router: Router) {}

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const token = this.api.getToken();

    if (token) {
      req = req.clone({
        setHeaders: {
          Authorization: `Bearer ${token}`
        }
      });
    }

    return next.handle(req).pipe(
      catchError((err: any) => {
        if (err instanceof HttpErrorResponse) {
          if (err.status === 401) {
            this.toast.warning("Login session has expired, please login again!", "WARNING", 5000);
            this.router.navigate(['login']);
          }
        }
        return throwError(() => new Error("Some other errors occurred"));
      })
    );
  }
}
