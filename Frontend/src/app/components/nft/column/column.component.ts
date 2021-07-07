import { Component, OnInit, Input } from '@angular/core';
import { TokenStateService } from 'src/app/dapp/token-state/token-state.service';

@Component({
  selector: 'app-column',
  templateUrl: './column.component.html',
  styleUrls: ['./column.component.less']
})
export class ColumnComponent implements OnInit {

  @Input()
  tokens: TokenStateService[];

  @Input()
  title: string;

  constructor() { }

  ngOnInit(): void {
  }

}
