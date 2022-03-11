import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { HttpHandlerService } from '../shared/http-handler.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  constructor(private httpHandlerService: HttpHandlerService, private router: Router, private route: ActivatedRoute) {
  }

  ngOnInit(): void {
  }

  success:boolean=false;
  email:string="";

  switchSuccess(value:any){
    this.success=value['success'];
    this.email=value['email'];
  }
}
