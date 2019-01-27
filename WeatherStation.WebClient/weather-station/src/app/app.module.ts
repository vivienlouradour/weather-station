import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { ChartModule } from 'angular-highcharts';

import { AppComponent } from './app.component';
import { SensorMonitorComponent } from './sensor-monitor/sensor-monitor.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { LastRecordComponent } from './last-record/last-record.component';
import { TopbarComponent } from './topbar/topbar.component';
import { ChartComponent } from './chart/chart.component';
import { ApiService } from './services/api.service';

@NgModule({
  declarations: [
    AppComponent,
    SensorMonitorComponent,
    SidebarComponent,
    LastRecordComponent,
    TopbarComponent,
    ChartComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    ChartModule
  ],
  providers: [
    ApiService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
