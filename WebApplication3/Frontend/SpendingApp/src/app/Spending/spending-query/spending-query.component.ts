import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormGroup, FormControl, Validators, FormArray, AbstractControl } from '@angular/forms';
import { SubUserService } from 'src/app/shared/subuser.service';
import * as _ from 'lodash';
import { ThisReceiver } from '@angular/compiler';
import { HttpHandlerService } from 'src/app/shared/http-handler.service';

@Component({
  selector: 'app-spending-query',
  templateUrl: './spending-query.component.html',
  styleUrls: ['./spending-query.component.scss']
})
export class SpendingQueryComponent implements OnInit {

  constructor(public subUserService: SubUserService,private httpHandlerService: HttpHandlerService ) { }
  @Input() isSubUserQuery=false;
  public recommendedCategories: string[] = [];
  errormsg:string="";
  @Output() newQueryEvent = new EventEmitter<any>();
  get minDateData() {
    return this.queryData.get('mindate');
  }

  get maxDateData() {
    return this.queryData.get('maxdate');
  }

  get minPriceData() {
    return this.queryData.get('minprice');
  }

  get maxPriceData() {
    return this.queryData.get('maxprice');
  }

  get categoriesData() {
    return (this.queryData.get('categories') as FormArray);
  }

  get subuseridData() {
    return this.queryData.get('subuserid');
  }

  get categoriesFormControls(){
    return (this.queryData.get('categories') as FormArray).controls;
  }

  get orderbyData(){
    return this.queryData.get('orderby');
  }

  get groupbyData(){
    return this.queryData.get('groupby');
  }

  get subusersData(){
    return (this.queryData.get('subusersFilter') as FormArray);
  }

  get subusersFormControls(){
    return (this.queryData.get('subusersFilter') as FormArray).controls;
  }

  
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
    this.submitQuery();
  }

  queryData = new FormGroup({
    mindate: new FormControl(''),
    maxdate: new FormControl(''),
    minprice: new FormControl('', [
      Validators.pattern("^[1-9][0-9]*$")]),
    maxprice: new FormControl('', [
      Validators.pattern("^[1-9][0-9]*$")]),
    categories:new FormArray([]),
    subusersFilter:new FormArray([]),
    orderby:new FormControl(''),
    groupby:new FormControl('category'),
    subuserid: new FormControl(''),
  });

  addNewCategoryField(){
    this.categoriesData.push(new FormControl(''));
  }

  addNewSubUserFilter(){
    this.subusersData.push(new FormControl(''));
  }

  submitQuery(){
    let queryCopyData =this.queryData.value;
    if(this.queryData.invalid){  
      this.showError("Invalid data");
      return;
    }
    if(this.minDateData?.value=="") queryCopyData['mindate']=new Date(0).toISOString().slice(0, 10);
    if(this.maxDateData?.value=="") queryCopyData['maxdate']=new Date(2800, 1, 1).toISOString().slice(0, 10);
    if(this.minPriceData?.value=="") queryCopyData['minprice']=1;
    if(this.maxPriceData?.value=="") queryCopyData['maxprice']=10000000;
    if(this.orderbyData?.value=="") queryCopyData['orderby']="date";
    queryCopyData['subuserid']=this.subUserService.currentSubUser.id;
    this.newQueryEvent.next(queryCopyData);
  }

  showError(msg:string){
    this.errormsg=msg;
    setTimeout( () => {
      this.errormsg = "";
    }, 4000);
  }

  deleteCategoryField(i:number){
      this.categoriesData.removeAt(i);
  }

  deleteSubUserField(i:number){
    this.subusersData.removeAt(i);
}

}
