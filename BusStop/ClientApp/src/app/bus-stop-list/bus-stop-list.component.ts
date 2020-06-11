import { Component, OnInit } from '@angular/core';
import { BusStopDto } from '../bus-stop/bus-stop.component';
import { retry, catchError } from 'rxjs/operators';
import { BusStopService } from './bus-stop-service.service';
import { HttpResponse, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';

@Component({
  selector: 'app-bus-stop-list',
  templateUrl: './bus-stop-list.component.html',
  styleUrls: ['./bus-stop-list.component.css']
})
export class BusStopListComponent implements OnInit {
  busStops: BusStopDto[] = [];
  private busStopIdsForDemo: number[] = [ 1, 2 ]

  constructor(private busStopService: BusStopService) { }

  ngOnInit() {
    for (let id of this.busStopIdsForDemo) {
      this.getArrivalsOnLoop(id);
    }
  }

  private getArrivalsOnLoop(busStopId: number): void {
    this.busStopService.getNextArrivalTimes(busStopId)
      .pipe(retry(3), catchError(this.dataRetrievalError))
      .subscribe((data: HttpResponse<BusStopDto>) => {
        let busStop = data.body;
        let existingStopIndex = this.busStops.findIndex(x => x.name == busStop.name);
        if (existingStopIndex >= 0) {
          this.busStops.splice(existingStopIndex, 1);
        }
        this.busStops.push(data.body);
        // note: we would probably want to sort by name, but since the stops are numbered, this was easier than writing/importing a string comparison that takes numeric tokens into account -- overkill for this demo
        this.busStops.sort((first, second) => first.id > second.id ? 1 : first.id == second.id ? 0 : -1);
      });
    let now = new Date();
    let secondsToNextMinute = 60 - now.getSeconds();
    // using long polling, because we can calculate exactly when the next update will occur; no need for a websocket
    setTimeout(() => this.getArrivalsOnLoop(busStopId), secondsToNextMinute * 1000);
  }

  private dataRetrievalError(error: HttpErrorResponse): Observable<any> {
    // if this were a more involved application, we could show a modal here
    return throwError('An error occurred attempting to retrieve bus stop data.');
  }
}
