import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SensorMonitorComponent } from './sensor-monitor.component';

describe('SensorMonitorComponent', () => {
  let component: SensorMonitorComponent;
  let fixture: ComponentFixture<SensorMonitorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SensorMonitorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SensorMonitorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
