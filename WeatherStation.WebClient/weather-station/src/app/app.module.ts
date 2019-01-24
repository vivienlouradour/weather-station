import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { SensorMonitorComponent } from './sensor-monitor/sensor-monitor.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { LastRecordComponent } from './last-record/last-record.component';

@NgModule({
  declarations: [
    AppComponent,
    SensorMonitorComponent,
    SidebarComponent,
    LastRecordComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
