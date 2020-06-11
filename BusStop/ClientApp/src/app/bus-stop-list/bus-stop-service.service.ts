import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { BusStopDto } from '../bus-stop/bus-stop.component';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BusStopService {

  constructor(private http: HttpClient) { }

  public getNextArrivalTimes(busStopId: number): Observable<HttpResponse<BusStopDto>> {
    return this.http.get<BusStopDto>('api/busstop/' + busStopId, { observe: 'response' });
  }
}
