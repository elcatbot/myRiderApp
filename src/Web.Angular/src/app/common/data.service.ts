import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  constructor(private http: HttpClient) { }

  get(url: string, params?: any) : Observable<any> {
    return this.http.get(url, { params })
      .pipe(
        tap((res: any) => {
          return res;
        }),
        catchError((error) => {
          throw error;
        })
      );
  }
}
