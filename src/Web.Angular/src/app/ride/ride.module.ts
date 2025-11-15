import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { RideRoutingModule } from './ride-routing.module';
import { RideListComponent } from './ride-list/ride-list.component';
import { RideDetailComponent } from './ride-detail/ride-detail.component';
import { RideBookingComponent } from './ride-booking/ride-booking.component';
import { RideTrackingComponent } from './ride-tracking/ride-tracking.component';
import { RideService } from './ride.service';


@NgModule({
  declarations: [
    RideListComponent,
    RideDetailComponent,
    RideBookingComponent,
    RideTrackingComponent
  ],
  imports: [
    CommonModule,
    RideRoutingModule
  ],
  providers: [
    RideService
  ]
})
export class RideModule { }
