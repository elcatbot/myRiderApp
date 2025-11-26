import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, catchError, map, Observable, tap, throwError } from 'rxjs';
import { ILoginResponse } from './models/ride/ILoginResponse';
import { IAuthService } from './models/auth/IAuthService';
import { IAuthStatus } from './models/auth/IAuthStatus';
import { jwtDecode } from 'jwt-decode';

export const defaultAuthStatus: IAuthStatus = {
  isAuthenticated: false, 
  userId: '',  
  userRole: 'Guest' 
};

@Injectable({
  providedIn: 'root'
})
export class AuthService implements IAuthService {

  private baseUrl = 'http://localhost:5001/api/auth';
  readonly authStatus$ = new BehaviorSubject<IAuthStatus>(defaultAuthStatus);

  constructor(private http: HttpClient) { }

  login(email: string, password: string) : Observable<boolean> {
    const loginResponse$ = this.http.post(this.baseUrl + '/login', { email, password })
      .pipe(
        map((value: any) => {
          localStorage.setItem('jwt', value.accessToken);
          const status = this.getAuthStatusFromToken(value.accessToken);
          this.authStatus$.next(status);
          return status.isAuthenticated;
        })
      );

      loginResponse$.subscribe({
        error: (err) => {
          return throwError(() => err)
        }
      });

      return loginResponse$;
  }

  register(fullName: string, role: string,  email: string, password: string) : Observable<ILoginResponse> {
    return this.http.post(this.baseUrl + '/register', { name: fullName, role, email, password })
      .pipe(
        tap((value: any) => {
          return value;
        }),
        catchError((error) => {
          throw error;
        })
      );
  }

  private getAuthStatusFromToken(token: string): IAuthStatus {
    var decoded: any = jwtDecode(token);
    return {
      isAuthenticated: decoded.email ? true : false,
      userId: decoded.sub,
      userRole: decoded.role
    };
  }

}
