import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { LocationResult, LocationService } from '../../common/services/location.service';
import { debounceTime, distinctUntilChanged, switchMap, tap } from 'rxjs';
import { RideService } from '../ride.service';
import { Router } from '@angular/router';
import { SignalRService } from '../../common/services/signal-r.service';
import { IRide } from '../../common/models/ride/IRide';

@Component({
  selector: 'app-ride-request',
  standalone: false,
  templateUrl: './ride-request.component.html',
  styleUrls: ['./ride-request.component.scss']
})
export class RideRequestComponent implements OnInit {
  
  rideForm!: FormGroup;
  rideError!: string | undefined;

  selectedPickup!: LocationResult;
  selectedDropoff!: LocationResult;

  pickupResults: LocationResult[] = [];
  dropoffResults: LocationResult[] = [];

  driverAssigned: any;
  isWaitingForDriver = false;

  constructor(
    private formBuilder: FormBuilder, 
    private locationService: LocationService,
    private rideService: RideService,
    private router: Router,
    private hubService: SignalRService
  ) { }

  ngOnInit(): void {
    this.rideForm = this.formBuilder.group({
      pickup: ['', Validators.required],
      dropoff: ['', Validators.required],
      rideType: ['standard', Validators.required],
      fare: ['0', Validators.required]
    });
    this.locationValueChanges();
  }

  onRequestRide(submitedForm: FormGroup): void {
    this.isWaitingForDriver = true;
    this.rideService.requestRide(this.selectedPickup, this.selectedDropoff, submitedForm.value.fare)
      .subscribe(res => {
        this.hubService.startConnection("http://localhost:5053/hub/ride", res.id);
        this.hubService.onDriverAssigned(driver => {
          this.driverAssigned = driver;
          this.isWaitingForDriver = false; // stop loading
        });
      });
  }

  selectResult(location: LocationResult, val: string) {
    if(val === 'pickup') {
      this.rideForm.patchValue({ pickup: location.display_name}, { emitEvent: false });
      this.selectedPickup = location;
      this.pickupResults = [];
    } else {
      this.rideForm.patchValue({ dropoff: location.display_name}, { emitEvent: false });
      this.selectedDropoff = location,
      this.dropoffResults = [];
    }
  }

  private locationValueChanges () {
    this.rideForm.get('pickup')?.valueChanges
      .pipe(
        debounceTime(500),
        distinctUntilChanged(),
        switchMap( val => this.locationService.forwardGeocode( val! ))
      )
      .subscribe({
        next: res => this.pickupResults = res,
        error: () => this.pickupResults = []
      });

    this.rideForm.get('dropoff')?.valueChanges
      .pipe(
        debounceTime(500),
        distinctUntilChanged(),
        switchMap( val => this.locationService.forwardGeocode( val! ))
      )
      .subscribe({
        next: res => this.dropoffResults = res,
        error: () => this.dropoffResults = []
      });
  }

  cancelRide() {
    this.isWaitingForDriver = false;
    // Optionally call backend to cancel ride
    console.log('Ride request cancelled');
  }
}
