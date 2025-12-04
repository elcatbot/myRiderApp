import { Injectable } from '@angular/core';
import { DataService } from '../common/services/data.service';
import { Observable, tap } from 'rxjs';
import { IRidePageIndex } from '../common/models/ride/IRidePageIndex';
import { LocationResult } from '../common/services/location.service';
import { AuthService } from '../common/services/auth.service';
import { ILocation } from '../common/models/ride/ILocation';
import { IRide } from '../common/models/ride/IRide';

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

  getRideById(rideId: string) : Observable<IRide> {
    let url = `${this.baseUrl}/${rideId}`;
    return this.dataService.get(url)
      .pipe(
        tap((res: IRide) => {
          return res;
        })
      );
  }

  requestRide(pickupRequest: LocationResult, dropoffRequest: LocationResult, fare: number) : Observable<IRide>{
    let riderId;
    this.authService.authStatus$.pipe(tap((status) => { riderId = status.userId })).subscribe();
    let pickup : ILocation = { latitude: pickupRequest.lat, longitude: pickupRequest.lon };
    let dropoff : ILocation = { latitude: dropoffRequest.lat, longitude: dropoffRequest.lon };
    
    return this.dataService.post(this.baseUrl, {riderId, pickup, dropoff, fare })
      .pipe(
        tap((res) => {
          return res;
        })
      );
  }

  cancelRide(rideId: string) : Observable<void> {
    let url = `${this.baseUrl}/cancel`;
    return this.dataService.put(url, { rideId });
  }
}
