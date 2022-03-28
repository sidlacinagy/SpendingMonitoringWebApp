import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { HttpHandlerService } from 'src/app/shared/http-handler.service';
import { SubUser } from 'src/app/shared/subuser.model';
import { SubUserService } from 'src/app/shared/subuser.service';

@Component({
  selector: 'app-sub-user-edit-form',
  templateUrl: './sub-user-edit-form.component.html',
  styleUrls: ['./sub-user-edit-form.component.scss']
})
export class SubUserEditFormComponent implements OnInit {

  constructor(private httpHandlerService: HttpHandlerService, public subUserService: SubUserService, private modalService: NgbModal) { }
  @Output() newResponseResult = new EventEmitter<string>();
  @Input()
  subuser: SubUser = new SubUser("", "");
  renamedUserName: string = "";
  ngOnInit(): void {
    this.renamedUserName = this.subuser.name;
  }

  deleteSubUser() {
    let subUserData = new FormGroup({
      subuserid: new FormControl(this.subuser.id)
    });
    this.httpHandlerService.deleteSubUser(subUserData.value).subscribe({
      next: () => { this.subUserService.getAllSubUsersFromServer(); 
                    this.newResponseResult.emit("Successfully deleted " + this.subuser.name);
                    if(this.subUserService.currentSubUser.id=this.subuser.id){
                      this.subUserService.currentSubUser=this.subUserService.subusers[0];
                    }
                  },
      error: (e) => { this.newResponseResult.emit(e.message); }
    })
  }

  renameSubUser() {
    let subUserData = new FormGroup({
      subuserid: new FormControl(this.subuser.id),
      name: new FormControl(this.renamedUserName)
    });
    this.httpHandlerService.renameSubUser(subUserData.value).subscribe({
      next: () => {
        this.subUserService.getAllSubUsersFromServer(); 
        this.newResponseResult.emit("Successfully renamed "+this.subuser.name+" to "+this.renamedUserName);
      },
      error: (e) => { this.newResponseResult.emit(e.message); }
    })
  }

  openModal(content: any) {
    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' });
  }

}
