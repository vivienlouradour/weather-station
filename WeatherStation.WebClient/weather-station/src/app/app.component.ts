import { Component } from '@angular/core';

export interface WeatherRecord{ //Sans export, il faudrait l'importer dans tous les fichiers qui l'utilise
  dateTime: Date;
  temperature: string;
  humidity: string;
}

export interface WeatherBroadcaster{
  broadcasterId: number;
  broadcasterName: string;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'weather-station';
}
