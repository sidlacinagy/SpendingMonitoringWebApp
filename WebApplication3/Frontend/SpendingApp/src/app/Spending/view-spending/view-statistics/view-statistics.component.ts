import { Component, Input, OnInit } from '@angular/core';
import { SpendingStatistic } from 'src/app/shared/spending-statistic.model';

@Component({
  selector: 'app-view-statistics',
  templateUrl: './view-statistics.component.html',
  styleUrls: ['./view-statistics.component.scss']
})
export class ViewStatisticsComponent implements OnInit {

  constructor() { }
  @Input()
  public statistics: SpendingStatistic[] = [];
  ngOnInit(): void {
  }

  numberWithCommas(x:number) {
    return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
  }
}
