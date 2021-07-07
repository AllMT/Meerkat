import { Component, OnInit } from '@angular/core';
import { T3Service } from 'src/app/dapp/t3/t3.service';
import { TokenStateService } from 'src/app/dapp/token-state/token-state.service';

@Component({
  selector: 'app-my-tokens',
  templateUrl: './my-tokens.component.html',
  styleUrls: ['./my-tokens.component.less']
})
export class MyTokensComponent implements OnInit {
  tokens: TokenStateService[] = [];

  constructor(private T3: T3Service) { }

  ngOnInit(): void {

    this.T3.walletInitalized.subscribe(data =>
      {
        if(data)
        {
          this.T3.getMyTokensFromContract()
          .then(result => this.tokens = result);
        }
      });
  }
}
