import { Injectable } from '@angular/core';
import { HttpHandlerService } from './http-handler.service';
import { SubUser } from './subuser.model';
@Injectable({ providedIn: 'root' })
export class SubUserService {
  constructor(private httpHandlerService: HttpHandlerService) {}
  
  public subusers: SubUser[] = [];
  public currentSubUser?: SubUser = undefined;
  getAllSubUsersFromServer() {
  this.httpHandlerService.getSubUsers().subscribe({
    next: (data) => {
      this.setSubUsersFromData(data);
      if (this.currentSubUser == undefined && this.subusers.length != 0) {
        this.currentSubUser = this.subusers[0];
      }
    },
    error: () => { }
  })
}

  private setSubUsersFromData(data: Array<any>){
  let subusers: SubUser[] = [];
  data.forEach(element => {
    let subUser = new SubUser(element.subUserName, element.subUserId);
    subusers.push(subUser);
  });
  this.subusers = subusers;
}

  
}


