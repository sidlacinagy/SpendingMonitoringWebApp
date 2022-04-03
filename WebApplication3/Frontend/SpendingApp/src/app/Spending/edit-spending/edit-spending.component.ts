import { ThisReceiver } from '@angular/compiler';
import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { HttpHandlerService } from 'src/app/shared/http-handler.service';
import { Spending } from 'src/app/shared/spending.model';
import { SubUserService } from 'src/app/shared/subuser.service';

@Component({
  selector: 'app-edit-spending',
  templateUrl: './edit-spending.component.html',
  styleUrls: ['./edit-spending.component.scss']
})
export class EditSpendingComponent implements OnInit {

  constructor(private httpHandlerService: HttpHandlerService,public subUserService: SubUserService) { }

  queryData:any;
  errormsg:string="";
  previousPageSpendings:Spending[]=[];
  presentPageSpendings:Spending[]=[];
  nextPageSpendings:Spending[]=[];
  public recommendedCategories: string[] = [];
  currentpage:number=1;
  ngOnInit(): void {
    this.httpHandlerService.getRecommendedCategories(this.subUserService.currentSubUser.id).subscribe({
      next: (data) => {
        this.recommendedCategories=data;
        this.recommendedCategories.sort(function (a, b) {
          return a.toLowerCase().localeCompare(b.toLowerCase());
      });
      },
      error: (e) => { 
        this.errormsg = "Error loading recommended categories";}
    })
  }

  switchToNext(){
    this.currentpage=this.currentpage+1;
    this.previousPageSpendings=this.presentPageSpendings;
    this.presentPageSpendings=this.nextPageSpendings;
    this.queryData["page"]=this.currentpage+1;
    this.httpHandlerService.getSpendingByQuery(this.queryData).subscribe({
      next: (data) => {
        this.nextPageSpendings=this.getSpendingArrayFromData(data);
      },
      error: (e) => { 
        this.errormsg = "Error loading spending";}
    })


  }

  switchToPrevious(){
    this.currentpage=this.currentpage-1;
    this.nextPageSpendings=this.presentPageSpendings;
    this.presentPageSpendings=this.previousPageSpendings;
    this.queryData["page"]=this.currentpage-1;
    this.httpHandlerService.getSpendingByQuery(this.queryData).subscribe({
      next: (data) => {
        this.previousPageSpendings=this.getSpendingArrayFromData(data);
      },
      error: (e) => { 
        this.errormsg = "Error loading spending";}
    })

  }
  
  submitNewQuery(data:any){
      this.currentpage=1;
      this.queryData=data;
      this.queryData["page"]=this.currentpage;
      this.previousPageSpendings=[];
      this.presentPageSpendings=[];
      this.nextPageSpendings=[];
      this.httpHandlerService.getSpendingByQuery(this.queryData).subscribe({
        next: (data) => {
          this.presentPageSpendings=this.getSpendingArrayFromData(data);
          this.queryData["page"]=this.currentpage+1;
          this.httpHandlerService.getSpendingByQuery(this.queryData).subscribe({
            next: (data) => {
              this.nextPageSpendings=this.getSpendingArrayFromData(data);
            },
            error: (e) => { 
              this.errormsg = "Error loading spending";}
          });
        },
        error: (e) => { 
          this.errormsg = "Error loading spending";}
      });
  }

  getSpendingArrayFromData(data:Array<any>){
    let spendings: Spending[] = [];
    data.forEach(e => {
      let spending = new Spending(e.Id,e.subUser,e.product,e.productCategory,e.price,new Date(e.date));
      spendings.push(spending);
    });
    return spendings;
  }

  refreshSpendings(event:any){
    this.queryData["page"]=this.currentpage-1;
    this.httpHandlerService.getSpendingByQuery(this.queryData).subscribe({
      next: (data) => {
        this.previousPageSpendings=this.getSpendingArrayFromData(data);
      },
      error: (e) => { 
        this.errormsg = "Error loading spending";}
    })
    this.queryData["page"]=this.currentpage;
    this.httpHandlerService.getSpendingByQuery(this.queryData).subscribe({
      next: (data) => {
        this.presentPageSpendings=this.getSpendingArrayFromData(data);
      },
      error: (e) => { 
        this.errormsg = "Error loading spending";}
    })
    this.queryData["page"]=this.currentpage+1;
    this.httpHandlerService.getSpendingByQuery(this.queryData).subscribe({
      next: (data) => {
        this.nextPageSpendings=this.getSpendingArrayFromData(data);
      },
      error: (e) => { 
        this.errormsg = "Error loading spending";}
    })
  }
}
