import { Component, OnInit, Input } from '@angular/core';
import { TokenStateService } from 'src/app/dapp/token-state/token-state.service';

@Component({
  selector: 'app-row',
  templateUrl: './row.component.html',
  styleUrls: ['./row.component.less']
})
export class RowComponent implements OnInit {

  @Input()
  tokens: TokenStateService[];

  @Input()
  title: string;

  constructor() { }

  ngOnInit(): void {
  }

}
