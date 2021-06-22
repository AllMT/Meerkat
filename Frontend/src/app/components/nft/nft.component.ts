import { Component, OnInit } from '@angular/core';
import { T3Service } from 'src/app/dapp/t3/t3.service';
import { TokenStateService } from 'src/app/dapp/token-state/token-state.service';

@Component({
  selector: 'app-nft',
  templateUrl: './nft.component.html',
  styleUrls: ['./nft.component.less']
})
export class NftComponent implements OnInit {

  tokens: TokenStateService[];

  constructor(private T3: T3Service) { }

  ngOnInit(): void {
    this.T3.tokens.subscribe(data =>
      {
        this.tokens = data;
        console.log(this.tokens);
      });
  }
}
