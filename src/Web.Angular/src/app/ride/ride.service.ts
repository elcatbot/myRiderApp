import { Injectable } from '@angular/core';
import { DataService } from '../common/data.service';
import { Observable, tap } from 'rxjs';
import { IRidePageIndex } from '../common/models/ride/IRidePageIndex';

@Injectable({
  providedIn: 'root'
})
export class RideService {

  private baseUrl = 'http://localhost:5053/api/rides';

  constructor(private dataService: DataService) { }

  getRides(pageIndex: number, pageSize: number) : Observable<IRidePageIndex> {
    let url = `${this.baseUrl}?pageIndex=${pageIndex}&pageSize=${pageSize}`;
    return this.dataService.get(url)
      .pipe(
        tap((res: IRidePageIndex) => {
          return res;
        })
      );
  }
}
