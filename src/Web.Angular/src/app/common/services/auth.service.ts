import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, catchError, map, Observable, tap, throwError } from 'rxjs';
import { ILoginResponse } from '../models/ride/ILoginResponse';
import { IAuthService } from '../models/auth/IAuthService';
import { IAuthStatus } from '../models/auth/IAuthStatus';
import { jwtDecode } from 'jwt-decode';
import { Router } from '@angular/router';

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

  constructor(private http: HttpClient,  private router: Router) {
    if(this.hasExpiredToken()){
      this.logout();
    } else {
      this.authStatus$.next(this.getAuthStatusFromToken())
    }
  }

  login(email: string, password: string) : Observable<boolean> {
    return this.http.post(this.baseUrl + '/login', { email, password })
      .pipe(
        map((value: any) => {
          this.setTokens(value.accessToken, value.refreshToken);
          const status = this.getAuthStatusFromToken();
          this.authStatus$.next(status);
          return status.isAuthenticated;
        }),
        catchError((error) => {
          return throwError(() => error)
        })
      );
  }

  register(
    fullName: string, 
    role: string,  
    email: string, 
    password: string) : Observable<ILoginResponse> {
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

  logout() : void {
    setTimeout(() => {
      this.clearTokens();
      this.authStatus$.next(defaultAuthStatus);
    }, 0);
  }

  getToken(key : string) : string {
    return localStorage.getItem(key) ?? '';
  }

  setTokens(token: string, refresh: string) {
    localStorage.setItem('jwt', token);
    localStorage.setItem('jwt_refresh', refresh);
    this.scheduleRefresh(token);
  }

  clearTokens() {
      localStorage.removeItem('jwt');
      localStorage.removeItem('jwt_refresh');
  }

  refreshToken(): Observable<any> {
    const refreshToken = this.getToken('jwt_refresh');
    if(!refreshToken) {
      return throwError(() => new Error('No refresh token'));
    }
    return this.http.post(this.baseUrl + '/token/refresh', { refreshToken })
      .pipe(
        tap((res: any) => this.setTokens(res.accessToken, res.refreshToken))
      );
  }
  
  hasExpiredToken(): boolean {
    const jwt = this.getToken('jwt');
    if(jwt){
      const payload = jwtDecode(jwt) as any;
      return Date.now() >= payload.exp * 1000;
    }
    return true;
  }

  private getAuthStatusFromToken(): IAuthStatus {
    var decoded: any = jwtDecode(this.getToken('jwt'));
    return {
      isAuthenticated: decoded.email ? true : false,
      userId: decoded.sub,
      userRole: decoded.role
    };
  }

  private scheduleRefresh(token: string): void {
    const payload = JSON.parse(atob(token.split('.')[1]));
    const exp = payload.exp * 1000; // convert to ms
    const now = Date.now();

    // Refresh 1 minute before expiry (adjust as needed)
    const refreshDelay = exp - now - 60_000;

    if (refreshDelay > 0) {
      setTimeout(() => {
        this.refreshToken().subscribe({
          next: (response) => {
            this.setTokens(response.accessToken, response.refreshToken);
          },
          error: () => {
            this.clearTokens();
            this.router.navigate(['/login']);
          }
        });
      }, refreshDelay);
    }
  }
}
