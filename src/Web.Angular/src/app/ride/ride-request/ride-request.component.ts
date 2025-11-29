import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { LocationResult, LocationService } from '../../common/location.service';
import { debounceTime, distinctUntilChanged, switchMap } from 'rxjs';
import { RideService } from '../ride.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-ride-request',
  standalone: false,
  templateUrl: './ride-request.component.html',
  styleUrls: ['./ride-request.component.scss']
})
export class RideRequestComponent implements OnInit {
  
  rideForm!: FormGroup;
  rideError!: string | undefined;

  // searchPickup = new FormControl('');
  // searchDropoff = new FormControl('');

  selectedPickup!: LocationResult;
  selectedDropoff!: LocationResult;

  pickupResults: LocationResult[] = [];
  dropoffResults: LocationResult[] = [];

  constructor(
    private formBuilder: FormBuilder, 
    private locationService: LocationService,
    private rideService: RideService,
    private router: Router
  ) { }

  ngOnInit(): void {

    this.rideForm = this.formBuilder.group({
      pickup: ['', Validators.required],
      dropoff: ['', Validators.required],
      rideType: ['standard', Validators.required]
    });

    this.locationValueChanges();
  }

  onRequestRide(): void {
    if (this.rideForm.valid) {
      this.rideService.requestRide(this.selectedPickup, this.selectedDropoff)
       .subscribe({
        next: () => this.router.navigate(['/ride']),
        error: (err) => this.rideError = err
      });
    }
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
}
