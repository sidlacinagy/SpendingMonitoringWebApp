import { Component, OnInit } from '@angular/core';
import { HttpHandlerService } from '../shared/http-handler.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  constructor(
    private httpHandlerService: HttpHandlerService
  ) { }

  ngOnInit(): void {
  }
  test(){
    
    this.httpHandlerService.testAuth().subscribe( as => console.log(as));
  }

}
