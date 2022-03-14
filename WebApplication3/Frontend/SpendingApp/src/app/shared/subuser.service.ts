import { Injectable } from '@angular/core';
import { HttpHandlerService } from './http-handler.service';
import { SubUser } from './SubUser.model';
@Injectable({ providedIn: 'root' })
export class SubUserService {
  constructor(private httpHandlerService: HttpHandlerService) {}
  subusers:SubUser[]=[];

  getAllSubUsersFromServer(){
        this.httpHandlerService.getSubUsers().subscribe({
            next: (data) => {this.setSubUsersFromData(data)},
            error: () => {}
        })
  }

  private setSubUsersFromData(data: Array<any>){
    let subusers:SubUser[]=[];
    data.forEach(element => {
        let subUser=new SubUser(element.subUserName,element.subUserId);
        subusers.push(subUser);
    });
    this.subusers=subusers;
  }

  
}
