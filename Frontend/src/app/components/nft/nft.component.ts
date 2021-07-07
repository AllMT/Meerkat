import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';
import { T3Service } from 'src/app/dapp/t3/t3.service';
import { TokenStateService } from 'src/app/dapp/token-state/token-state.service';

@Component({
  selector: 'app-nft',
  templateUrl: './nft.component.html',
  styleUrls: ['./nft.component.less']
})
export class NftComponent implements OnInit {

  @Input()
  token: TokenStateService;

  constructor(private router: Router, private T3: T3Service) { }

  ngOnInit(): void { }

  details(tokenIndex: string)
  {
    this.router.navigate(["nft/" + tokenIndex])
  }
}
