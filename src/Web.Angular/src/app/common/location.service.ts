import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { map, Observable } from 'rxjs';
import { environment } from '../../environments/environments';

export interface LocationResult {
  place_id: number;
  lat: string;
  lon: string;
  display_name: string;
}

@Injectable({ providedIn: 'root' })
export class LocationService {
  
  private baseUrl = environment.locationApiBaseUrl;

  constructor(private http: HttpClient) {}

  forwardGeocode(query: string): Observable<LocationResult[]> {
    const params = new HttpParams()
      .set('q', query)
      .set('format', 'json')
      .set('addressdetails', '1')
      .set('limit', '5');
    return this.http.get<LocationResult[]>(`${this.baseUrl}/search`, { params }).pipe(
      map(results =>
        results.map( r => ({
          place_id: r.place_id,
          lat: r.lat,
          lon: r.lon,
          display_name: r.display_name
        }))
      )
    );
  }

  reverseGeocode(lat: number, lon: number): Observable<any> {
    const params = new HttpParams()
      .set('lat', lat.toString())
      .set('lon', lon.toString())
      .set('format', 'json')
      .set('addressdetails', '1');
    return this.http.get(`${this.baseUrl}/reverse`, { params });
  }
}
