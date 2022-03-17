import { Component, OnInit } from '@angular/core';
import { SubUserService } from 'src/app/shared/subuser.service';

@Component({
  selector: 'app-home-base',
  templateUrl: './home-base.component.html',
  styleUrls: ['./home-base.component.scss']
})
export class HomeBaseComponent implements OnInit {

  constructor( public subUserService: SubUserService,) { }

  ngOnInit(): void {
  }

}
