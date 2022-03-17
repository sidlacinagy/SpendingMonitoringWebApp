import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HttpHandlerService } from '../shared/http-handler.service';
import { SubUser } from '../shared/subuser.model';
import { SubUserService } from '../shared/subuser.service';


@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {

  constructor(private httpHandlerService: HttpHandlerService, public subUserService: SubUserService, private router: Router) { }

  ngOnInit(): void {
    this.subUserService.getAllSubUsersFromServer();
    
  }

  logout() {
    this.httpHandlerService.logout().subscribe({
      next: () => this.router.navigate(['/login']),
      error: (e) => { }
    })
  }

  print(){
    this.subUserService.getAllSubUsersFromServer();
    console.log(this.subUserService.subusers);
  }

  switchUser(subuser:SubUser){
    this.subUserService.currentSubUser=subuser;
  }

}
