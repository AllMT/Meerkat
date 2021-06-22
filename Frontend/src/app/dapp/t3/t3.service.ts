import { Injectable } from '@angular/core';
import NeoLineN3Init from '../../neo-line/neo-line-n3-init';
import { NeoLineN3Interface } from '../../neo-line/neo-line-n3-interface';
import { TokenStateService } from '../token-state/token-state.service';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class T3Service {

  CONTRACT_HASH = "0x60ea23e08660403301dc604d37a0b7ded87ec0b7";

  public neoLine: NeoLineN3Interface
  public tokens: BehaviorSubject<TokenStateService[]>;

  constructor() {

    window.addEventListener("NEOLine.NEO.EVENT.READY", async () => {
      this.neoLine = await NeoLineN3Init();
      console.log("initialised");

    });

    window.addEventListener("NEOLine.NEO.EVENT.BLOCK_HEIGHT_CHANGED", () => {
      this.getLatestTokensFromContract();
    })

    this.tokens = new BehaviorSubject([]);
   }

   getLatestTokensFromContract()
   {
      this.neoLine.invokeRead(
        { 
          scriptHash: this.CONTRACT_HASH,
          operation: "testGetLatestArtTokensByIndex",
          args: [],
          signers: []
        }).then(result => 
        {
          console.log(result);

          //{ "type": "Integer", "value": "1"}



          // (<any>result.stack[0].value).forEach(element => {
          //   var token = new TokenStateService(this.neoLine, element.value);

          //   var index = this.tokens.value.findIndex(t => t.Id === token.Id); 

          //   if(index === -1)
          //   {
          //     this.tokens.next(this.tokens.getValue().concat([token]));
          //   }
          // });
        });
   }
}; 
