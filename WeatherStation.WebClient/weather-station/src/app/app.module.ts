import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { SensorMonitorComponent } from './sensor-monitor/sensor-monitor.component';

@NgModule({
  declarations: [
    AppComponent,
    SensorMonitorComponent
  ],
  imports: [
    BrowserModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
