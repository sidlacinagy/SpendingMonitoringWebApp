import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpHandlerService } from '../shared/http-handler.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  constructor(private httpHandlerService: HttpHandlerService, private router: Router, private route: ActivatedRoute) {
  }

  errormsg: string = "";
  fieldTextType = false;
  userData = new FormGroup({
    email: new FormControl('', [
      Validators.required,
      Validators.pattern("^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$")]),
    password: new FormControl('', [
      Validators.required])
  });
  get emailData() {
    return this.userData.get('email');
  }

  get passwordData() {
    return this.userData.get('password');
  }

  ngOnInit(): void {
  }

  onSubmit() {
    if (this.userData.invalid) {
      this.errormsg = "Invalid data";
      return;
    }
    this.httpHandlerService.login(this.userData.value).subscribe({
      next: () => this.router.navigate(['home'], { relativeTo: this.route }),
      error: (e) => { this.errormsg = e.message; }
    })
  }

  toggleFieldTextType() {
    this.fieldTextType = !this.fieldTextType;
  }
}
