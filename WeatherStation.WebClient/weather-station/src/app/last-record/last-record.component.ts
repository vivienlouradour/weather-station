import { Component, OnInit, Input } from '@angular/core';
import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { WeatherRecord, WeatherBroadcaster } from '../app.component';
import { Observable } from 'rxjs';
import config from '../../assets/config.json';

@Component({
  selector: 'app-last-record',
  templateUrl: './last-record.component.html',
  styleUrls: ['./last-record.component.scss']
})

export class LastRecordComponent implements OnInit {

  //record: Observable<WeatherRecord>;
  record: WeatherRecord;
  broadcasterList: WeatherBroadcaster[];
  @Input() broadcaster: string;


  constructor(private httpClient: HttpClient) { }

  ngOnInit() {
    let urlBro = config.apiUrl + '/broadcaster/';
    this.httpClient
      .get<WeatherBroadcaster[]>(urlBro)
      .subscribe(resp => {
        this.broadcasterList = resp;
      });

    let url = config.apiUrl + '/record/' + this.broadcaster;
    
    this.httpClient
      .get<WeatherRecord>(url)
      .subscribe(resp => {
        this.record = resp
      });
  }

  

}
