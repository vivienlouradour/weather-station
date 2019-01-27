import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-chart',
  templateUrl: './chart.component.html',
  styleUrls: ['./chart.component.scss']
})
export class ChartComponent implements OnInit {

  // chart = new Chart({
  //   chart: {
  //     type: 'line'
  //   },
  //   title: {
  //     text: 'Linechart'
  //   },
  //   credits: {
  //     enabled: false
  //   },
  //   series: [
  //     {
  //       name: 'Line 1',
  //       data: [1, 2, 3]
  //     }
  //   ]
  // });

  constructor() { }

  ngOnInit() {
  
  }

  add(){
    // this.chart.addPoint(Math.floor(Math.random() * 10));
  }

}
