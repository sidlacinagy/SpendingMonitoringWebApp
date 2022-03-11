import { Component, Input, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-registersuccess',
  templateUrl: './registersuccess.component.html',
  styleUrls: ['./registersuccess.component.scss']
})
export class RegistersuccessComponent implements OnInit {

  constructor(private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
  }

  @Input() email:string="";

}
