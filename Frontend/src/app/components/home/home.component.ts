import { Component, OnInit } from '@angular/core';
import { T3Service } from 'src/app/dapp/t3/t3.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.less']
})
export class HomeComponent implements OnInit {

  constructor(private T3: T3Service) { }

  ngOnInit(): void {
  }

}
