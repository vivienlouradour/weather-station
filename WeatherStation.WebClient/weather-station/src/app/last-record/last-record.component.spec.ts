import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LastRecordComponent } from './last-record.component';

describe('LastRecordComponent', () => {
  let component: LastRecordComponent;
  let fixture: ComponentFixture<LastRecordComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LastRecordComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LastRecordComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
