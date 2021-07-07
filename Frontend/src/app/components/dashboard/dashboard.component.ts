import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.less']
})
export class DashboardComponent implements OnInit {

  @Input()
  showMyTokens: boolean;

  @Input()
  showHomeButton: boolean;
  
  constructor() { }

  ngOnInit(): void {
  }

}
