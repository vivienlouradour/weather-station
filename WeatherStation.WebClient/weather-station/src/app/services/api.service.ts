import config from '../../assets/config.json';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { WeatherRecord } from '../app.component.js';
import { Subject, Observable, interval } from 'rxjs';

@Injectable()
export class ApiService{
    private apiUrl: string = config.apiUrl;
    private lastRecord: WeatherRecord;
    lastRecordSubject = new Subject<WeatherRecord>();
    

    constructor(private httpClient: HttpClient){
        this.getLastRecord();
        interval(config.apiRefreshIntervalInSeconds * 1000).subscribe(() => this.getLastRecord());
    }


    getRecordRange(){
        let url = this.apiUrl + '/record/' + config.broadcaster + '/2018-11-05T00:00:00/2018-11-10T00:00:00'
        return this.httpClient
            .get<WeatherRecord[]>(url);
         
    }

    private getLastRecord(){
        this.httpClient
        .get<WeatherRecord>(this.apiUrl + '/record/' + config.broadcaster)
        .subscribe(
            (response) => {
                this.lastRecord = response;
                this.emitLastRecord()
            },
            (error) => {
                console.log('Can\'t get last record from ' + config.broadcaster + ' : ' + JSON.stringify(error));
            }
        );
    }

    emitLastRecord(){
        this.lastRecordSubject.next(this.lastRecord);
    }
}