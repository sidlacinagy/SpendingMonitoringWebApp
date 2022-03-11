import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpHandlerService } from '../shared/http-handler.service';

@Component({
  selector: 'app-resetpassword',
  templateUrl: './resetpassword.component.html',
  styleUrls: ['./resetpassword.component.scss']
})
export class ResetpasswordComponent implements OnInit {

  constructor(private route: ActivatedRoute,private httpHandlerService: HttpHandlerService,private router:Router) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.token = params['token'];
    });
  }
  token: string = "";
  errormsg: string = "";

  userData = new FormGroup({
    token: new FormControl("",[]),
    password: new FormControl('', [
      Validators.required]),  
    password2: new FormControl('', [
      Validators.required])
  });
  
  get tokenData() {
    return this.userData.get('token');
  }

  get passwordData() {
    return this.userData.get('password');
  }

  get password2Data() {
    return this.userData.get('password2');
  }

  fieldTextType1 = false;
  fieldTextType2 = false;

  onSubmit() {
    this.tokenData?.setValue(this.token);
    if (this.userData.invalid) {
      this.errormsg = "Invalid data";
      return;
    }
    if (this.passwordData?.value!=this.password2Data?.value) {
      this.errormsg = "Passwords not matching";
      return;
    }
    this.httpHandlerService.resetpassword(this.userData.value).subscribe({
      next: () => {this.router.navigate(['/login']);} ,
      error: (e) => { this.errormsg = e.message; }
    })
  }

  toggleFieldTextType1() {
    this.fieldTextType1 = !this.fieldTextType1;
  }

  toggleFieldTextType2() {
    this.fieldTextType2 = !this.fieldTextType2;
  }

}
