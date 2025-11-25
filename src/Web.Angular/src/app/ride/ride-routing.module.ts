import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RideListComponent } from './ride-list/ride-list.component';
import { RideDetailComponent } from './ride-detail/ride-detail.component';
import { RideTrackingComponent } from './ride-tracking/ride-tracking.component';

const routes: Routes = [
  { path: '', component: RideListComponent },
  { path: ':id', component: RideDetailComponent },
  { path: 'track-ride/:id', component: RideTrackingComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class RideRoutingModule { }
