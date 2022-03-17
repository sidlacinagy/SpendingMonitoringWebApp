import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { HttpHandlerService } from '../shared/http-handler.service';
import { SubUserService } from '../shared/subuser.service';

@Component({
  selector: 'app-sub-user-edit',
  templateUrl: './sub-user-edit.component.html',
  styleUrls: ['./sub-user-edit.component.scss']
})
export class SubUserEditComponent implements OnInit {

  constructor(private httpHandlerService: HttpHandlerService,public subUserService: SubUserService) { }
  errormsg:string="";
  newUserName:string="";
  ngOnInit(): void {
  }

  createNewUser(){
    let subUserData = new FormGroup({
      name: new FormControl(this.newUserName)
    });
    this.httpHandlerService.addSubUser(subUserData.value).subscribe({
      next: () => {this.subUserService.getAllSubUsersFromServer();
      this.newUserName=""},
      error: (e) => { this.errormsg = e.message; }
    })
  }



}
