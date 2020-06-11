import { TestBed, inject } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { BusStopService } from './bus-stop-service.service';
import { HttpResponse } from '@angular/common/http';
import { BusStopDto } from '../bus-stop/bus-stop.component';

describe('BusStopServiceService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [HttpClientTestingModule]
  }));

  it('should be created', () => {
    const service: BusStopService = TestBed.get(BusStopService);
    expect(service).toBeTruthy();
  });

  it('should return a bus stop dto', () => {
    const httpMock: HttpTestingController = TestBed.get(HttpTestingController)
    const service: BusStopService = TestBed.get(BusStopService);
    service.getNextArrivalTimes(1)
      .subscribe((data) => {
        data.body
      });

    let request = httpMock.expectOne('api/busstop/1');
    expect(request.request.method).toEqual('GET');

    request.flush({
      data: {
        body: {
          id: 1,
          name: 'Bus Stop 1',
          routes: []
        } as BusStopDto
      }
    });
  });
});
