import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { catchError, map, Observable, Subject, switchMap, throwError } from 'rxjs';
import { HttpHandlerService } from './shared/http-handler.service';
import { ActivatedRoute, Router } from '@angular/router';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

  constructor(private httpHandlerService: HttpHandlerService, private router: Router, private route: ActivatedRoute ) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<Object>> {
    return next.handle(request).pipe(
      catchError((error: HttpErrorResponse) => {
        if(error.status===401)
        {
          return this.handle401Error(request, next);
        }
        return throwError(() => new Error(error.error));
      })
    )
  }

  private handle401Error(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<Object>> {
    return this.httpHandlerService.resetToken().pipe(
      switchMap(() => {
        return next.handle(request);
      }),
      catchError(() => {
        this.router.navigate(['login'], { relativeTo: this.route });
        return throwError(() => new Error("Unauthorized"));
      })
    )
  }
}
