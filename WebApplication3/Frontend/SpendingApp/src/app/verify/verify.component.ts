import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { HttpHandlerService } from '../shared/http-handler.service';

@Component({
  selector: 'app-verify',
  templateUrl: './verify.component.html',
  styleUrls: ['./verify.component.scss']
})
export class VerifyComponent implements OnInit {

  constructor(private httpHandlerService: HttpHandlerService, private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.httpHandlerService.verify({token:params['token']}).subscribe({
        next: () => this.router.navigate(['/login']),
        error: () => this.router.navigate(['/login'])
      });
    });

}

}
