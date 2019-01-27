import { Component, OnInit, Input } from '@angular/core';
import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { WeatherRecord, WeatherBroadcaster } from '../app.component';
import { Observable, Subscription } from 'rxjs';
import config from '../../assets/config.json';
import { ApiService } from '../services/api.service';

@Component({
  selector: 'app-last-record',
  templateUrl: './last-record.component.html',
  styleUrls: ['./last-record.component.scss']
})

export class LastRecordComponent implements OnInit {

  broadcaster = config.broadcaster;
  recordSubscription: Subscription;
  record: WeatherRecord;
  errorMessage: string;


  constructor(private apiService: ApiService ) { }

  ngOnInit() {
    this.apiService.getRecordRange().subscribe((response) => {console.log("RESPONSE : " + response);});
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
    
  }

  ngOnDestroy(){
    this.recordSubscription.unsubscribe();
  }

  

}
