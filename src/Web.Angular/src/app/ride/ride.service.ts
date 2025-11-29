import { Injectable } from '@angular/core';
import { DataService } from '../common/data.service';
import { Observable, tap } from 'rxjs';
import { IRidePageIndex } from '../common/models/ride/IRidePageIndex';
import { LocationResult } from '../common/location.service';
import { AuthService } from '../common/auth.service';
import { ILocation } from '../common/models/ride/ILocation';

@Injectable({
  providedIn: 'root'
})
export class RideService {

  private baseUrl = 'http://localhost:5002/api/rides';

  constructor(private dataService: DataService, private authService: AuthService) { }

  getRides(pageIndex: number, pageSize: number) : Observable<IRidePageIndex> {
    let url = `${this.baseUrl}?pageIndex=${pageIndex}&pageSize=${pageSize}`;
    return this.dataService.get(url)
      .pipe(
        tap((res: IRidePageIndex) => {
          return res;
        })
      );
  }

  requestRide(pickupRequest: LocationResult, dropoffRequest: LocationResult) : Observable<void>{
    let riderId;
    this.authService.authStatus$.pipe(tap((status) => { riderId = status.userId })).subscribe();
    let pickup : ILocation = { latitude: pickupRequest.lat, longitude: pickupRequest.lon };
    let dropoff : ILocation = { latitude: dropoffRequest.lat, longitude: dropoffRequest.lon };
    
    return this.dataService.post(this.baseUrl, {riderId, pickup, dropoff })
      .pipe(
        tap((res) => {
          return res;
        })
      );
  }
}
