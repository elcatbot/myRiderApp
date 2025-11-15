import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map, Observable, tap } from 'rxjs';
import { ILoginResponse } from './models/ILoginResponse';

@Injectable({
  providedIn: 'root'
})
export class SecurityService {

  private baseUrl = 'http://localhost:5001/api/auth';

  constructor(private http: HttpClient) { }

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
}
