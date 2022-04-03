import { Component, OnInit } from '@angular/core';
import { HttpHandlerService } from 'src/app/shared/http-handler.service';
import { SpendingStatistic } from 'src/app/shared/spending-statistic.model';
import { SubUserService } from 'src/app/shared/subuser.service';

@Component({
  selector: 'app-view-spending',
  templateUrl: './view-spending.component.html',
  styleUrls: ['./view-spending.component.scss']
})
export class ViewSpendingComponent implements OnInit {

  constructor(private httpHandlerService: HttpHandlerService, public subUserService: SubUserService) { }

  ngOnInit(): void {
  }

errormsg:String="";
statistics1:SpendingStatistic[]=[];
statistics2:SpendingStatistic[]=[];
currentquery1:any;
currentquery2:any;
isComperable:boolean=false;
  submitNewQuery1(data:any){
    console.log(data);
    this.currentquery1=data;
    this.httpHandlerService.getSpendingStatisticsByQuery(data).subscribe({
      next: (data) => {
        this.statistics1=this.createStatitisticFromData(data,this.currentquery1.groupby);
        this.isComperable=false;
        if(this.currentquery1.groupby==this.currentquery2.groupby) {
        this.isComperable=true;
        this.mergeStatistics();
        }
      },
      error: (e) => { 
        this.errormsg = "Error loading spending";}
    })
  }

  submitNewQuery2(data:any){
    this.currentquery2=data;
    this.httpHandlerService.getSpendingStatisticsByQuery(data).subscribe({
      next: (data) => {
        this.statistics2=this.createStatitisticFromData(data,this.currentquery2.groupby);
        this.isComperable=false;
        if(this.currentquery1.groupby==this.currentquery2.groupby) {
        this.isComperable=true;
        this.mergeStatistics();
        }
      },
      error: (e) => { 
        this.errormsg = "Error loading spending";}
    })
  }

  numberWithCommas(x:number) {
    return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
  }

  createStatitisticFromData(data:Array<any>,groupby:string){
    let statistics: SpendingStatistic[] = [];
    data.forEach(element => {
      let keyAsCorrectString:string ="";
      if(groupby=="category"){
        keyAsCorrectString=element.key;
      }
      else if(groupby=="date"){
        let year=element.key.substr(element.key.indexOf("Year = ")+"Year = ".length,4);
        let month=element.key.substr(element.key.indexOf("Month = ")+"Month = ".length,2);
        if(month.includes(" ")) month="0"+month.substr(0,1);
        keyAsCorrectString=year+"-"+month;
      }
      else if(groupby=="price"){
        let minPrice=this.numberWithCommas(Math.pow(10,Number(element.key)));
        let maxPrice=this.numberWithCommas(Math.pow(10,(Number((element.key))+1))-1);
        keyAsCorrectString=minPrice+' - '+maxPrice;
      }
      let statistic = new SpendingStatistic(keyAsCorrectString,element.sum,element.count);
      statistics.push(statistic);
    });
   
    this.sortStatistics(statistics,groupby);
    return statistics;
  }



  mergeStatistics(){
    this.statistics1.forEach(element => {
      let elref=element;
      if(this.statistics2.find(element=>element.key==elref.key)==undefined){
        this.statistics2.push(new SpendingStatistic(element.key,0,0));
      }
    });
    this.statistics2.forEach(element => {
      let elref=element;
      if(this.statistics1.find(element=>element.key==elref.key)==undefined){
        this.statistics1.push(new SpendingStatistic(element.key,0,0));
      }
    });

    this.statistics1.forEach(element => {
      let elref=element;
      let secondElRef=this.statistics2.find(element=>element.key==elref.key);
      if(secondElRef!=undefined){
        if(elref.count==0 && secondElRef.count==0){
          var index1 = this.statistics1.indexOf(elref);
          this.statistics1.splice(index1,1);
          var index2 = this.statistics2.indexOf(secondElRef);
          this.statistics2.splice(index2,1);
        }
      }
    });
    this.statistics1=this.sortStatistics(this.statistics1,this.currentquery1.groupby);
    this.statistics2=this.sortStatistics(this.statistics2,this.currentquery2.groupby);
  }

  sortStatistics(statistics:SpendingStatistic[],groupby:string){
    if(groupby=="category"){
      statistics.sort(function (a, b) {
        return a.key.toLowerCase().localeCompare(b.key.toLowerCase());
    });
    }
    else if(groupby=="date"){
      statistics.sort((a,b) => (new Date(a.key) > new Date(b.key)) ? 1 : ((new Date(b.key) > new Date(a.key)) ? -1 : 0));
    }
    else if(groupby=="price"){
      statistics.sort((a,b) => {
        let aNumber=a.key.replaceAll(",","");
        aNumber=Number(aNumber.replace(" - ",""));
        let bNumber=b.key.replaceAll(",","");
        bNumber=Number(bNumber.replace(" - ",""));
        return (aNumber > bNumber ? 1 : 
        bNumber > aNumber ? -1 : 0)}
      );
    }
    return statistics;
  }
}
