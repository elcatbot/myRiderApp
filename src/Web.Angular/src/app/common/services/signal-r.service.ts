import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { environment } from '../../../environments/environments';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {

  private baseUrl: string = environment.rideApiBaseUrl;
  private hubConnection!: signalR.HubConnection;

  constructor() { }

    public startConnection(api: string, rideId: string) {
      let route;
      if(api === 'ride') {
        route = 'hub/ride'
      }
      else {
        route = 'hub/driver'
      }
      this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(`${this.baseUrl}/${route}`)
      .withAutomaticReconnect()
      .build();

      this.hubConnection.start()
        .then(() => {
          console.log('Connected to RideHub');
          console.log('rideId: ' + rideId);
          this.hubConnection.invoke('JoinRideGroup', rideId);
        })
        .catch(err => console.error('Error connecting to hub', err));
    }

    onDriverAssigned(callback: (driver: any) => void) {
      this.hubConnection.on('DriverAssignedRide', callback);
    } 

    onRideStatusChanged(callback: (ride: any) => void) {
      this.hubConnection.on('RideStatusChanged', callback);
    } 
}
