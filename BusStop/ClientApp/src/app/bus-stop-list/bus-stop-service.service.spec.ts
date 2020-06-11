import { TestBed } from '@angular/core/testing';

import { BusStopServiceService } from './bus-stop-service.service';

describe('BusStopServiceService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: BusStopServiceService = TestBed.get(BusStopServiceService);
    expect(service).toBeTruthy();
  });
});
