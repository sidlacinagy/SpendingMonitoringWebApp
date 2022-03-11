import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { HttpHandlerService } from 'src/app/shared/http-handler.service';

@Component({
  selector: 'app-registerform',
  templateUrl: './registerform.component.html',
  styleUrls: ['./registerform.component.scss']
})
export class RegisterformComponent implements OnInit {

  constructor(private httpHandlerService: HttpHandlerService) {
  }

  ngOnInit(): void {
  }

  errormsg: string = "";
  @Output() newItemEvent = new EventEmitter<any>();
  userData = new FormGroup({
    email: new FormControl('', [
      Validators.required,
      Validators.pattern("^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$")]),
    password: new FormControl('', [
      Validators.required]),
    password2: new FormControl('', [
      Validators.required])
  });
  get emailData() {
    return this.userData.get('email');
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
    if (this.userData.invalid) {
      this.errormsg = "Invalid data";
      return;
    }
    if (this.passwordData?.value!=this.password2Data?.value) {
      this.errormsg = "Passwords not matching";
      return;
    }
    this.httpHandlerService.register(this.userData.value).subscribe({
      next: () => this.newItemEvent.emit({success:true, email:this.emailData?.value}),
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
