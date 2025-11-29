import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { RideRoutingModule } from './ride-routing.module';
import { RideListComponent } from './ride-list/ride-list.component';
import { RideDetailComponent } from './ride-detail/ride-detail.component';
import { RideTrackingComponent } from './ride-tracking/ride-tracking.component';
import { RideRequestComponent } from './ride-request/ride-request.component';
import { RideService } from './ride.service';
import { ReactiveFormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    RideListComponent,
    RideDetailComponent,
    RideTrackingComponent,
    RideRequestComponent
  ],
  imports: [
    CommonModule,
    RideRoutingModule,
    ReactiveFormsModule,
  ],
  providers: [
    RideService
  ]
})
export class RideModule { }
