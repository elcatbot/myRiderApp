import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {

  private hubConnection!: signalR.HubConnection;

  constructor() { }

    public startConnection(url: string, rideId: string) {
      this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(url)
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
}
