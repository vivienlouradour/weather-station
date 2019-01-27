import { Component, OnInit } from '@angular/core';
import { Component, OnInit, Input } from '@angular/core';
import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { WeatherRecord, WeatherBroadcaster } from '../app.component';
import { Observable, Subscription } from 'rxjs';
import config from '../../assets/config.json';
import { ApiService } from '../services/api.service';

@Component({
  selector: 'app-sensor-monitor',
  templateUrl: './sensor-monitor.component.html',
  styleUrls: ['./sensor-monitor.component.scss']
})
export class SensorMonitorComponent implements OnInit {
  broadcaster = config.broadcaster;
  recordSubscription: Subscription;
  record: WeatherRecord;
  errorMessage: string;

  constructor(private apiService: ApiService ) { }

  ngOnInit() {
    this.broadcaster = config.broadcaster;
    this.recordSubscription = this.apiService.lastRecordSubject.subscribe(
      (lastRecord: WeatherRecord) => {
        this.errorMessage = '';        
        this.record = lastRecord;
      },
      (error) => {
        this.errorMessage = this.errorMessage;
      }
    );
    
    // this.apiService.getLastRecord();
  }

  ngOnDestroy(){
    this.recordSubscription.unsubscribe();
  }

}
