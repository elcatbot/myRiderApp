import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { RideRoutingModule } from './ride-routing.module';
import { RideListComponent } from './ride-list/ride-list.component';
import { RideDetailComponent } from './ride-detail/ride-detail.component';
import { RideTrackingComponent } from './ride-tracking/ride-tracking.component';
import { RideRequestComponent } from './ride-request/ride-request.component';
import { RideService } from './ride.service';
import { ReactiveFormsModule } from '@angular/forms';
import { SpinnerComponent } from '../common/components/spinner/spinner.component';
import { AppModule } from '../app.module';
import { AppCommonModule } from '../common/app-common.module';


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
    AppCommonModule
  ],
  providers: [
    RideService
  ]
})
export class RideModule { }
