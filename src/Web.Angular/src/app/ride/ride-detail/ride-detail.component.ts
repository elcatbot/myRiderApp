import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { RideService } from '../ride.service';
import { IRide } from '../../common/models/ride/IRide';
import { SignalRService } from '../../common/services/signal-r.service';

@Component({
  selector: 'app-ride-detail',
  standalone: false,
  templateUrl: './ride-detail.component.html',
  styleUrl: './ride-detail.component.scss'
})
export class RideDetailComponent implements OnInit {
  ride!: IRide;
  isLoading = true;   // 👈 loading flag
  buttons = true;

  constructor(
    private route: ActivatedRoute, 
    private rideService: RideService,
    private hubService: SignalRService,
    private router: Router
  ) {}

  ngOnInit() {
    const rideId = this.route.snapshot.paramMap.get('id');
    if(rideId) {
      this.rideService.getRideById(rideId).subscribe(res => { 
        this.ride = res;
        this.isLoading = false;   // 👈 stop loading once data arrives
        if(this.ride.status === 'Completed' || this.ride.status === 'Cancelled') {
          this.buttons = false;
        }
      }); // Initial fetch from backend
      
      // this.hubService.startConnection('ride', rideId); // Connect to SignalR hub for live updates
      // this.hubService.onRideStatusChanged(updatedRide => { // Listen for ride status updates
      //   this.ride = updatedRide;
      //   console.log("Ride's been updated");
      // });
    }
  }

  cancelRide() {
    this.rideService.cancelRide(this.ride.id).subscribe(() => this.router.navigate(['/ride/']));
  }

  trackRide() {
    console.log('Tracking ride');
    // Could open map or tracking UI
  }
}
