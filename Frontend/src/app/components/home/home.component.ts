import { Component, OnInit } from '@angular/core';
import { T3Service } from 'src/app/dapp/t3/t3.service';
import { TokenStateService } from 'src/app/dapp/token-state/token-state.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.less']
})
export class HomeComponent implements OnInit {
  loading = true;
  
  latestTokens: TokenStateService[] = [];
  latestNFTTitle = "Latest NFTs";
  
  latestListedTokens: TokenStateService[] = [];
  latestListedNFTTitle = "Latest listed NFTs";

  constructor(private T3: T3Service) { }

  ngOnInit(): void {
    this.T3.walletInitalized.subscribe(data => 
      {
        this.loading = !data;

        if(data)
        {
          this.T3.getLatestTokensForMainPageFromContract()
          .then(result => this.latestTokens = result);

          this.T3.getLatestMarketTokensFromContract()
          .then(result => this.latestListedTokens = result);
        }
      });
  }
}
