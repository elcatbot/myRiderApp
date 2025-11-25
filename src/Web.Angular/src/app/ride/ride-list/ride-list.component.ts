import { Component, OnInit } from '@angular/core';
import { IRidePageIndex } from '../../common/models/ride/IRidePageIndex';
import { catchError, tap } from 'rxjs';
import { RideService } from '../ride.service';
import { IRide } from '../../common/models/ride/IRide';

@Component({
  selector: 'app-ride-list',
  standalone: false,
  templateUrl: './ride-list.component.html',
  styleUrl: './ride-list.component.scss'
})
export class RideListComponent implements OnInit {

    ridePage: IRidePageIndex = { pageIndex: 0, data: [], pageSize: 0, count: 0 };
    rides: IRide[] = [];

    constructor(private rideService: RideService) { }

    ngOnInit(): void {
      this.getRides();
    }

    getRides(){
      this.rideService.getRides(1, 10)
        .pipe(
          tap((data: IRidePageIndex) => {
            console.log('Rides fetched successfully');
            console.log('count:', data.count);
            console.log('Pickup:', data.data[0]?.pickUp.latitude);
          }),
          catchError(err => {
          console.error('Error fetching rides', err);
          throw err;
          })
        )
        .subscribe((data: IRidePageIndex) => {
          this.rides = data.data;
        });
  }
}
