import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { HttpHandlerService } from '../shared/http-handler.service';

@Component({
  selector: 'app-forgotpassword',
  templateUrl: './forgotpassword.component.html',
  styleUrls: ['./forgotpassword.component.scss']
})
export class ForgotpasswordComponent implements OnInit {

  errormsg:string="";
  userData = new FormGroup({
    email: new FormControl('', [
      Validators.required,
      Validators.pattern("^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$")]),
  });

  get emailData() {
    return this.userData.get('email');
  }

  constructor(private httpHandlerService: HttpHandlerService, private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
  }

  onSubmit(){
    if (this.userData.invalid) {
      this.errormsg = "Invalid data";
      return;
    }
    this.httpHandlerService.getResetToken(this.userData.value).subscribe({
      next: () => this.router.navigate(['/login'], { relativeTo: this.route }),
      error: (e) => { this.errormsg = e.message; }
    })
  }

}
