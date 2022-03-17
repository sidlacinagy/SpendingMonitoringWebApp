import { Component, Input, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { HttpHandlerService } from 'src/app/shared/http-handler.service';
import { SubUser } from 'src/app/shared/subuser.model';
import { SubUserService } from 'src/app/shared/subuser.service';

@Component({
  selector: 'app-sub-user-edit-form',
  templateUrl: './sub-user-edit-form.component.html',
  styleUrls: ['./sub-user-edit-form.component.scss']
})
export class SubUserEditFormComponent implements OnInit {

  constructor(private httpHandlerService: HttpHandlerService,public subUserService: SubUserService) { }

  @Input()
  subuser:SubUser=new SubUser("","");
  renamedUserName:string="";
  errormsg:string="";
  ngOnInit(): void {
    this.renamedUserName=this.subuser?.name;
  }

  deleteSubUser(){
    let subUserData = new FormGroup({
      subuserid: new FormControl(this.subuser.id)
    });
    this.httpHandlerService.deleteSubUser(subUserData.value).subscribe({
      next: () => {this.subUserService.getAllSubUsersFromServer()},
      error: (e) => { this.errormsg = e.message; }
    })
  }

  renameSubUser(){
    let subUserData = new FormGroup({
      subuserid: new FormControl(this.subuser.id),
      name: new FormControl(this.renamedUserName)
    });
    this.httpHandlerService.renameSubUser(subUserData.value).subscribe({
      next: () => {this.subUserService.getAllSubUsersFromServer();},
      error: (e) => { this.errormsg = e.message; }
    })
  }

}
