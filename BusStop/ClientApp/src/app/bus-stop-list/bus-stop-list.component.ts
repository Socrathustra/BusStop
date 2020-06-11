import { Component, OnInit } from '@angular/core';
import { BusStopDto } from '../bus-stop/bus-stop.component';

@Component({
  selector: 'app-bus-stop-list',
  templateUrl: './bus-stop-list.component.html',
  styleUrls: ['./bus-stop-list.component.css']
})
export class BusStopListComponent implements OnInit {
  busStops: BusStopDto[] = [
    {
      name: 'Bus Stop 1',
      routes: [
        {
          name: 'Route 1',
          arrivalTimes: [
            new Date(2020, 6, 10, 6, 30),
            new Date(2020, 6, 10, 6, 45),
          ]
        },
        {
          name: 'Route 2',
          arrivalTimes: [
            new Date(2020, 6, 10, 6, 32),
            new Date(2020, 6, 10, 6, 47),
          ]
        }
      ]
    },
    {
      name: 'Bus Stop 2',
      routes: [
        {
          name: 'Route 1',
          arrivalTimes: [
            new Date(2020, 6, 10, 6, 34),
            new Date(2020, 6, 10, 6, 49),
          ]
        },
        {
          name: 'Route 2',
          arrivalTimes: [
            new Date(2020, 6, 10, 6, 36),
            new Date(2020, 6, 10, 6, 51),
          ]
        }
      ]
    }];

  constructor() { }

  ngOnInit() {
  }

}
