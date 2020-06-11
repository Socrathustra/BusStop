import { Component, OnInit, Input } from '@angular/core';

export class BusStopDto {
  name: string;
  routes: RouteDto[];
}

export class RouteDto {
  name: string;
  arrivalTimes: Date[];
}

@Component({
  selector: 'app-bus-stop',
  templateUrl: './bus-stop.component.html',
  styleUrls: ['./bus-stop.component.css']
})
export class BusStopComponent implements OnInit {
  @Input() busStop: BusStopDto = {
    name: '',
    routes: []
  };

  constructor() { }

  ngOnInit() {
  }

}
