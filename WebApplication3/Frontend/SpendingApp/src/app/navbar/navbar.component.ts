import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { NgbModal, NgbModalConfig } from '@ng-bootstrap/ng-bootstrap';
import { HttpHandlerService } from '../shared/http-handler.service';
import { SubUser } from '../shared/subuser.model';
import { SubUserService } from '../shared/subuser.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {

  constructor(private httpHandlerService: HttpHandlerService, public subUserService: SubUserService, private router: Router,
    private modalService: NgbModal, private config: NgbModalConfig,) {
    config.backdrop = 'static';
    config.keyboard = false;
  }
  @ViewChild('createsubuser') createsubuserModal: ElementRef | undefined;
  @ViewChild('selectsubuser') selectsubuserModal: ElementRef | undefined;
  subusername = "";

  async ngOnInit(): Promise<void> {
    this.httpHandlerService.getSubUsers().subscribe({
      next: (data) => {
        this.subUserService.setSubUsersFromData(data);
        if (this.subUserService.subusers.length == 0) {
          this.openModal(this.createsubuserModal);
        }
        else{
          this.openModal(this.selectsubuserModal);
        }
      },
      error: () => { }
    })
  }

  logout() {
    this.httpHandlerService.logout().subscribe({
      next: () => this.router.navigate(['/login']),
      error: (e) => { }
    })
  }

  switchUser(subuser: SubUser) {
    this.subUserService.currentSubUser = subuser;
  }

  addSubuser() {
    this.httpHandlerService.addSubUser({ name: this.subusername }).subscribe({
      next: () => {
        this.httpHandlerService.getSubUsers().subscribe({
          next: (data) => {
            this.subUserService.setSubUsersFromData(data);
            this.subusername = "";
            this.subUserService.currentSubUser = this.subUserService.subusers[0];
          },
          error: () => { }
        })
      },
      error: (e) => { }
    })
  }

  openModal(content: any) {
    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title', size: 'lg' });
  }

}
