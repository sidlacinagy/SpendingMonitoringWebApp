import { Component, OnInit } from '@angular/core';
import { HttpHandlerService } from 'src/app/shared/http-handler.service';
import { SpendingStatistic } from 'src/app/shared/spending-statistic.model';
import { SubUserService } from 'src/app/shared/subuser.service';
import { HomeQueryData } from './home-query.model';

@Component({
  selector: 'app-home-base',
  templateUrl: './home-base.component.html',
  styleUrls: ['./home-base.component.scss']
})
export class HomeBaseComponent implements OnInit {

  constructor( public subUserService: SubUserService,private httpHandlerService: HttpHandlerService) { }

  statistics:SpendingStatistic[]=[];
  statisticsSortedByPrice:SpendingStatistic[]=[];
  statisticsSortedByCount:SpendingStatistic[]=[];
  errormsg:string="";
  subuserid?:string="";
  isOnlySubuser:boolean=false;
  maxDate:Date=new Date();
  minDate:Date=new Date();
  queryData:HomeQueryData=new HomeQueryData([],"category",this.maxDate.toISOString().slice(0, 10),1,"1970-01-01",10000000,[]);
  ngOnInit(): void {
    this.minDate.setMonth(this.minDate.getMonth()-1);
    this.queryData.mindate=this.minDate.toISOString().slice(0, 10);
      this.httpHandlerService.getSpendingStatisticsByQuery(this.queryData).subscribe(
        {
          next: (data) => {
            this.statistics=this.createStatitisticFromData(data);
            this.statisticsSortedByPrice=this.sortStatistics(this.statistics,"price");
            this.statisticsSortedByCount=this.sortStatistics(this.statistics,"count");
          },
          error: (e) => { 
            this.errormsg = "Error loading spending";}
        })
  }

  createStatitisticFromData(data:Array<any>){
    let statistics: SpendingStatistic[] = [];
    data.forEach(element => {
      let statistic = new SpendingStatistic(element.key,element.sum,element.count);
      statistics.push(statistic);
    });
    return statistics;
  }

  numberWithCommas(x:number) {
    return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
  }

  switchToSubUserQuery(){
    this.queryData.subusersFilter=[];
    this.queryData.subusersFilter.push(this.subUserService.currentSubUser.id);
    this.httpHandlerService.getSpendingStatisticsByQuery(this.queryData).subscribe(
      {
        next: (data) => {
          this.statistics=this.createStatitisticFromData(data);
          this.statisticsSortedByPrice=this.sortStatistics(this.statistics,"price");
          this.statisticsSortedByCount=this.sortStatistics(this.statistics,"count");
        },
        error: (e) => { 
          this.errormsg = "Error loading spending";}
      });
      this.isOnlySubuser=true;
  }

  switchToUserQuery(){
    this.queryData.subusersFilter=[];
    this.httpHandlerService.getSpendingStatisticsByQuery(this.queryData).subscribe(
      {
        next: (data) => {
          this.statistics=this.createStatitisticFromData(data);
          this.statisticsSortedByPrice=this.sortStatistics(this.statistics,"price");
          this.statisticsSortedByCount=this.sortStatistics(this.statistics,"count");
        },
        error: (e) => { 
          this.errormsg = "Error loading spending";}
      })
      this.isOnlySubuser=false;
  }

  sortStatistics(statistics:SpendingStatistic[],sortBy:string){
    let statisticsCopy = [...statistics];
    if(sortBy=="price"){
      statisticsCopy.sort((a,b) => {return b.sum - a.sum});
    }
    else if(sortBy=="count"){
      statisticsCopy.sort((a,b) => {return b.count - a.count});
    }
    return statisticsCopy;
  }
}


