import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable, switchMap } from 'rxjs';
import { AuthService } from './common/services/auth.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  token!: string;

  constructor(private authService: AuthService) {
    this.token = this.authService.getToken('jwt'); // or sessionStorage
  }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    // Get token from storage
    // console.log(this.token);
    if (this.token) {
      // Clone request and add Authorization header
      req = this.setHeaderToken(req, this.token);
    }
    return next.handle(req);
  }

  setHeaderToken(req: HttpRequest<any>, token: string) : HttpRequest<any>{
    return req.clone({
        setHeaders: { Authorization: `Bearer ${token}` }
      });
  }
}
