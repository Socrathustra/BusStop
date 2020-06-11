import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BusStopListComponent } from './bus-stop-list.component';

describe('BusStopListComponent', () => {
  let component: BusStopListComponent;
  let fixture: ComponentFixture<BusStopListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BusStopListComponent ]
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
});
