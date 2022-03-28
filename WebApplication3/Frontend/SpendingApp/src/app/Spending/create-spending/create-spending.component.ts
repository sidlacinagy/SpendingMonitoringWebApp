import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { HttpHandlerService } from 'src/app/shared/http-handler.service';
import { SubUserService } from 'src/app/shared/subuser.service';

@Component({
  selector: 'app-create-spending',
  templateUrl: './create-spending.component.html',
  styleUrls: ['./create-spending.component.scss']
})
export class CreateSpendingComponent implements OnInit {

  constructor(private httpHandlerService: HttpHandlerService, public subUserService: SubUserService,private modalService: NgbModal) { }
  errormsg = "";
  public recommendedCategories: string[] = [];
  newCategory="";
  spendingData = new FormGroup({
    product: new FormControl('', [
      Validators.required,]),
    productCategory: new FormControl('', [
      Validators.required]),
    price: new FormControl('', [
      Validators.required,
      Validators.pattern("^[1-9][0-9]*$")]),
    date: new FormControl(new Date().toISOString().slice(0, 10), [
      Validators.required]),
    subuserid: new FormControl(''),
  });


  initSpendingData() {
    this.spendingData.reset();
    this.productData?.setValue("");
    this.productCategoryData?.setValue("");
    this.priceData?.setValue("");
    this.subuseridData?.setValue("");
    this.dateData?.setValue(new Date().toISOString().slice(0, 10));
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
  }


  get productData() {
    return this.spendingData.get('product');
  }

  get productCategoryData() {
    return this.spendingData.get('productCategory');
  }

  get priceData() {
    return this.spendingData.get('price');
  }

  get dateData() {
    return this.spendingData.get('date');
  }

  get subuseridData() {
    return this.spendingData.get('subuserid');
  }


  createSpending() {
    if (this.spendingData.invalid) {
      this.errormsg = "Invalid data";
      return;
    }
    let date:Date=new Date(this.dateData?.value);
    if (this.dateData?.value===1) return;
    this.subuseridData?.setValue(this.subUserService.currentSubUser.id);
    this.httpHandlerService.addSpending(this.spendingData.value).subscribe({
      next: () => {
        this.errormsg = "Successfully created spending";
        this.initSpendingData();
      },
      error: (e) => { 
        this.errormsg = "Error created spending";}
    })
  }

  openModal(content: any) {
    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' });
  }

  addCategory(){
    this.recommendedCategories.push(this.newCategory);
    this.productCategoryData?.setValue(this.newCategory);
    this.recommendedCategories.sort(function (a, b) {
      return a.toLowerCase().localeCompare(b.toLowerCase());
  });
    this.newCategory="";
    console.log(this.recommendedCategories);
  }

}
