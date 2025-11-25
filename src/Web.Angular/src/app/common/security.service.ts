import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, catchError, map, Observable, tap } from 'rxjs';
import { ILoginResponse } from './models/ride/ILoginResponse';
import { IAuthService } from './models/auth/IAuthService';
import { IAuthStatus } from './models/auth/IAuthStatus';

@Injectable({
  providedIn: 'root'
})
export class SecurityService implements IAuthService {

  private baseUrl = 'http://localhost:5001/api/auth';

  constructor(private http: HttpClient) { }
  readonly authStatus$ = new BehaviorSubject<IAuthStatus>({ isAuthenticated: false, userId: '',  userRole: 'Guest' });

  login(email: string, password: string) : Observable<ILoginResponse> {
    return this.http.post(this.baseUrl + '/login', { email, password })
      .pipe(
        map((value: any) => {
          localStorage.setItem('jwt', value.accessToken);
          return value;
        }),
        tap((res: ILoginResponse) => {
          return res;
        }),
        catchError((error) => {
          throw error;
        })
      );
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
}
