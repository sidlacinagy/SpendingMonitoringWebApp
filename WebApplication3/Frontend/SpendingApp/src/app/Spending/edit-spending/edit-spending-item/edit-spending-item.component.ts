import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { findLastKey } from 'lodash';
import { HttpHandlerService } from 'src/app/shared/http-handler.service';
import { Spending } from 'src/app/shared/spending.model';
import { SubUserService } from 'src/app/shared/subuser.service';

@Component({
  selector: 'app-edit-spending-item',
  templateUrl: './edit-spending-item.component.html',
  styleUrls: ['./edit-spending-item.component.scss']
})
export class EditSpendingItemComponent implements OnInit {

  constructor(private httpHandlerService: HttpHandlerService,private modalService: NgbModal) { }
  isInEditMode:boolean=false;
  errormsg:string="";
  @Output() newItemEvent = new EventEmitter<any>();
  @Input()
  public recommendedCategories: string[] = [];

  @Input()
  spending: Spending = new Spending("", "","","",0,new Date);

  spendingData = new FormGroup({
    product: new FormControl('', [
      Validators.required,]),
    productCategory: new FormControl('', [
      Validators.required]),
    price: new FormControl('', [
      Validators.required,
      Validators.pattern("^[1-9][0-9]*$")]),
    date: new FormControl('', [
      Validators.required]),
    spendingId: new FormControl(''),
  });
  
  ngOnInit(): void {
    
  }

  initForm(){
    this.spendingData.get("product")?.setValue(this.spending.product);
    this.spendingData.get("productCategory")?.setValue(this.spending.productCategory);
    this.spendingData.get("price")?.setValue(this.spending.price);
    this.spendingData.get("date")?.setValue(this.spending.date.toISOString().slice(0, 10));
  }
  
  toggleEditMode(){
    this.isInEditMode=!this.isInEditMode;
    this.initForm();
  }

  onEditSave(){
    console.log(this.spending);
    this.spendingData.get("spendingId")?.setValue(this.spending.id);
    if(this.spendingData.invalid){
      this.showError("Invalid data");
      return;
    }
    this.httpHandlerService.updateSpending(this.spendingData.value).subscribe({
      next: (data) => {
        let date=new Date(this.spendingData.get("date")?.value);
        date.setHours(6);
        this.spending.date=date;
        this.spending.price=this.spendingData.get("price")?.value;
        this.spending.product=this.spendingData.get("product")?.value;
        this.spending.productCategory=this.spendingData.get("productCategory")?.value;
        console.log(this.spending);
        this.toggleEditMode();
      },
      error: (e) => { 
         this.showError("Error updating spending");}
    })

  }

  onDeleteSpending(){
    this.httpHandlerService.deleteSpending(this.spending.id).subscribe({
      next: (data) => {
        this.newItemEvent.next(true);
      },
      error: (e) => { 
         this.showError("Error deleting spending");}
    })
  }

  openModal(content: any) {
    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' });
  }

  showError(msg:string){
    this.errormsg=msg;
    setTimeout( () => {
      this.errormsg = "";
    }, 4000);
  }

}
