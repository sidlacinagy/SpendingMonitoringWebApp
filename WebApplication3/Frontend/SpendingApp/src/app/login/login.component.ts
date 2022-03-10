import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
import { HttpHandlerService } from '../shared/http-handler.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  constructor( private httpHandlerService: HttpHandlerService) { 
  }

  errormsg:string="";
  email="";
  password="";
  ngOnInit(): void {
  }

  onSubmit(form: NgForm){
    console.log(form);
    if(form.invalid){
      this.errormsg="Invalid data";
      return;
    }
    this.httpHandlerService.login(form.form.value).subscribe({
      next: (as) => console.log(as),
      error: (e) => {this.errormsg=e.message;}
    })
  }
  
}
