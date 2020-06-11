import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { BusStopListComponent } from './bus-stop-list.component';
import { BusStopComponent, BusStopDto } from '../bus-stop/bus-stop.component';
import { BusStopService } from './bus-stop-service.service';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

describe('BusStopListComponent', () => {
  let component: BusStopListComponent;
  let fixture: ComponentFixture<BusStopListComponent>;
  
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [BusStopListComponent, BusStopComponent],
      providers: [BusStopService],
      imports: [HttpClientTestingModule]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BusStopListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should request bus stop data', () => {
    const mockHttp: HttpTestingController = TestBed.get(HttpTestingController);

    component.busStops = [];

    let req1 = mockHttp.expectOne('api/busstop/1');
    expect(req1.request.method).toBe('GET');
    req1.flush({
      data: {
        body: {
          id: 1,
          name: 'Bus Stop 1',
          routes: []
        } as BusStopDto
      }
    });

    let req2 = mockHttp.expectOne('api/busstop/2');
    expect(req2.request.method).toBe('GET');
    req2.flush({
      data: {
        body: {
          id: 2,
          name: 'Bus Stop 2',
          routes: []
        } as BusStopDto
      }
    });

    mockHttp.verify();
  })
});
