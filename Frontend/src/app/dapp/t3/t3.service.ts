import { Injectable } from '@angular/core';
import NeoLineN3Init from '../../neo-line/neo-line-n3-init';
import { NeoLineN3Interface } from '../../neo-line/neo-line-n3-interface';
import { TokenStateService } from '../token-state/token-state.service';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class T3Service {

  CONTRACT = "e64a9f7193bfb5386f0a13f1c94259681cc482a3";
  GAS_CONTRACT = "d2a4cff31913016155e38e474a2c06d08be276cf"
  ACCOUNT_HASH = '';
  ACCOUNT_ADDRESS = new BehaviorSubject('');

  public walletInitalized = new BehaviorSubject(false);
  public neoLine: NeoLineN3Interface

  constructor() {

    window.addEventListener("NEOLine.NEO.EVENT.READY", async () => {
      this.neoLine = await NeoLineN3Init();
      
      if(this.neoLine)
      {

      this.neoLine.getAccount()
      .then(e => 
        {
          this.ACCOUNT_ADDRESS.next(e.address);

          this.neoLine.AddressToScriptHash(
            {
              address: e.address
            })
            .then(t => 
              {
                this.walletInitalized.next(true);
                this.ACCOUNT_HASH = t.scriptHash;
              });
        });

      }

    });

   }

   getLatestTokensForMainPageFromContract(): Promise<TokenStateService[]>
   {
      return this.neoLine.invokeRead(
        { 
          scriptHash: this.CONTRACT,
          operation: "getMainPageLatestTokens",
          args: [],
          signers: []
        }).then(result => 
        {
          var tokens = [];

          (<any>result.stack[0].value).forEach(element => {
            tokens.push(new TokenStateService(this.neoLine, element.value));
          });

          return tokens;
        });
   }

   getLatestTokensFromContract(): Promise<TokenStateService[]>
   {
      return this.neoLine.invokeRead(
        { 
          scriptHash: this.CONTRACT,
          operation: "getLatestTokens",
          args: [],
          signers: []
        }).then(result => 
        {
          var tokens = [];

          (<any>result.stack[0].value).forEach(element => {
            tokens.push(new TokenStateService(this.neoLine, element.value));
          });

          return tokens;
        });
   }

   getLatestMarketTokensFromContract(): Promise<TokenStateService[]>
   {
      return this.neoLine.invokeRead(
        { 
          scriptHash: this.CONTRACT,
          operation: "getMainPageMarketTokens",
          args: [],
          signers: []
        }).then(result => 
        {
          var tokens = [];

          (<any>result.stack[0].value).forEach(element => {
            tokens.push(new TokenStateService(this.neoLine, element.value));
          });

          return tokens;
        });
   }

   getLatestArtTokensFromContract(): Promise<TokenStateService[]>
   {
     if (this.neoLine === null || this.neoLine === undefined)
     {
       return new Promise(function(resolve, reject)  { resolve([])});
     }

      return this.neoLine.invokeRead(
        { 
          scriptHash: this.CONTRACT,
          operation: "getLatestArtTokensByIndex",
          args: [],
          signers: []
        }).then(result => 
        {
          var tokens = [];

          (<any>result.stack[0].value).forEach(element => {
            tokens.push(new TokenStateService(this.neoLine, element.value));
          });

          return tokens;
        });
   }

   getLatestCollectiblesTokensFromContract(): Promise<TokenStateService[]>
   {
     if (this.neoLine === null || this.neoLine === undefined)
     {
       return new Promise(function(resolve, reject)  { resolve([])});
     }

      return this.neoLine.invokeRead(
        { 
          scriptHash: this.CONTRACT,
          operation: "getLatestCollectiblesTokensByIndex",
          args: [],
          signers: []
        }).then(result => 
        {
          var tokens = [];

          (<any>result.stack[0].value).forEach(element => {
            tokens.push(new TokenStateService(this.neoLine, element.value));
          });

          return tokens;
        });
   }

   getLatestVirtualWorldsTokensFromContract(): Promise<TokenStateService[]>
   {
     if (this.neoLine === null || this.neoLine === undefined)
     {
       return new Promise(function(resolve, reject)  { resolve([])});
     }

      return this.neoLine.invokeRead(
        { 
          scriptHash: this.CONTRACT,
          operation: "getLatestVirtualWorldTokensByIndex",
          args: [],
          signers: []
        }).then(result => 
        {
          var tokens = [];

          (<any>result.stack[0].value).forEach(element => {
            tokens.push(new TokenStateService(this.neoLine, element.value));
          });

          return tokens;
        });
   }

   getLatestDomainNamesTokensFromContract(): Promise<TokenStateService[]>
   {
     if (this.neoLine === null || this.neoLine === undefined)
     {
       return new Promise(function(resolve, reject)  { resolve([])});
     }

      return this.neoLine.invokeRead(
        { 
          scriptHash: this.CONTRACT,
          operation: "getLatestDomainNamesTokensByIndex",
          args: [],
          signers: []
        }).then(result => 
        {
          var tokens = [];

          (<any>result.stack[0].value).forEach(element => {
            tokens.push(new TokenStateService(this.neoLine, element.value));
          });

          return tokens;
        });
   }

   getLatestSportsTokensFromContract(): Promise<TokenStateService[]>
   {
     if (this.neoLine === null || this.neoLine === undefined)
     {
       return new Promise(function(resolve, reject)  { resolve([])});
     }

      return this.neoLine.invokeRead(
        { 
          scriptHash: this.CONTRACT,
          operation: "getLatestSportsTokensByIndex",
          args: [],
          signers: []
        }).then(result => 
        {
          var tokens = [];

          (<any>result.stack[0].value).forEach(element => {
            tokens.push(new TokenStateService(this.neoLine, element.value));
          });

          return tokens;
        });
   }

   getLatestTradingCardsTokensFromContract(): Promise<TokenStateService[]>
   {
     if (this.neoLine === null || this.neoLine === undefined)
     {
       return new Promise(function(resolve, reject)  { resolve([])});
     }

      return this.neoLine.invokeRead(
        { 
          scriptHash: this.CONTRACT,
          operation: "getLatestTradingCardsTokensByIndex",
          args: [],
          signers: []
        }).then(result => 
        {
          var tokens = [];

          (<any>result.stack[0].value).forEach(element => {
            tokens.push(new TokenStateService(this.neoLine, element.value));
          });

          return tokens;
        });
   }

   getLatestUtilitiesTokensFromContract(): Promise<TokenStateService[]>
   {
     if (this.neoLine === null || this.neoLine === undefined)
     {
       return new Promise(function(resolve, reject)  { resolve([])});
     }

      return this.neoLine.invokeRead(
        { 
          scriptHash: this.CONTRACT,
          operation: "getLatestUtilitiyTokensByIndex",
          args: [],
          signers: []
        }).then(result => 
        {
          var tokens = [];

          (<any>result.stack[0].value).forEach(element => {
            tokens.push(new TokenStateService(this.neoLine, element.value));
          });

          return tokens;
        });
   }

   getLatestMusicTokensFromContract(): Promise<TokenStateService[]>
   {
     if (this.neoLine === null || this.neoLine === undefined)
     {
       return new Promise(function(resolve, reject)  { resolve([])});
     }

      return this.neoLine.invokeRead(
        { 
          scriptHash: this.CONTRACT,
          operation: "getLatestMusicTokensByIndex",
          args: [],
          signers: []
        }).then(result => 
        {
          var tokens = [];

          (<any>result.stack[0].value).forEach(element => {
            tokens.push(new TokenStateService(this.neoLine, element.value));
          });

          return tokens;
        });
   }

   getTokenByIndexFromContract(index: string): Promise<TokenStateService>
   {
     if (this.neoLine === null || this.neoLine === undefined)
     {
       return new Promise(function(resolve, reject)  { resolve(null)});
     }

      return this.neoLine.invokeRead(
        { 
          scriptHash: this.CONTRACT,
          operation: "getTokenByIndex",
          args: 
          [
            {
              type: "Integer",
              value: index
            }
          ],
          signers: []
        }).then(result => 
        {
          if(result.state == "HALT")
          {
            return new TokenStateService(this.neoLine, (<any>result.stack[0].value));
          }
          return null;
        });
   }

   getMyTokensFromContract(): Promise<TokenStateService[]>
   {
     if (this.neoLine === null || this.neoLine === undefined)
     {
       return new Promise(function(resolve, reject)  { resolve([])});
     }

      return this.neoLine.invokeRead(
        { 
          scriptHash: this.CONTRACT,
          operation: "getTokensFor",
          args: 
          [
            { 
              type: "Address", 
              value: this.ACCOUNT_ADDRESS.value
            }
          ],
          signers: []
        }).then(result => 
        {
          var tokens = [];

          (<any>result.stack[0].value).forEach(element => {
            tokens.push(new TokenStateService(this.neoLine, element.value));
          });

          return tokens;
        });
   }

   mint(data: string)
   {
     return this.neoLine.invoke(
       {
         scriptHash: this.CONTRACT,
         operation: "mintToken",
         args: [
           {
             type: "String",
             value: data
           }
         ],
         signers: [
           {
             account: this.ACCOUNT_HASH,
             scopes: 1
           }
         ],
         broadcastOverride: false,
        });
   }

   listToken(tokenId: string, options: string)
   {
    return this.neoLine.invoke(
      {
        scriptHash: this.CONTRACT,
        operation: "listToken",
        args: [
          {
            type: "ByteArray",
            value: tokenId
          },
          {
            type: "String",
            value: options
          }
        ],
        signers: [
          {
            account: this.ACCOUNT_HASH,
            scopes: 1
          }
        ],
        broadcastOverride: false,
       });
   }

   burn(tokenId: string)
   {
     return this.neoLine.invoke(
       {
         scriptHash: this.CONTRACT,
         operation: "burn",
         args: [
           {
             type: "ByteArray",
             value: tokenId
           }
         ],
         signers: [
           {
             account: this.ACCOUNT_HASH,
             scopes: 1
           }
         ],
         broadcastOverride: false,
        });
   }

   purchase(tokenId: string, value: string)
   {
     return this.neoLine.invoke(
       {
         scriptHash: this.GAS_CONTRACT,
         operation: "transfer",
         args: [
           {
             type: "Address",
             value: this.ACCOUNT_ADDRESS.value
            },
            {
              type: "Address",
              value: this.CONTRACT
             },
             {
              type: "Integer",
              value: value
             },
            {
              type: "Array",
              value: 
              [
                {
                  type: "String",
                  value: "PurchaseToken"
                },
                {
                  type: "ByteArray",
                  value: tokenId
                }
              ] 
            }
         ],
         signers: [
           {
             account: this.ACCOUNT_HASH,
             scopes: 1
           }
         ],
         broadcastOverride: false,
        });
   }

   removeListing(tokenId: string)
   {
     return this.neoLine.invoke(
       {
         scriptHash: this.CONTRACT,
         operation: "removeMarketListing",
         args: [
           {
             type: "ByteArray",
             value: tokenId
           }
         ],
         signers: [
           {
             account: this.ACCOUNT_HASH,
             scopes: 1
           }
         ],
         broadcastOverride: false,
        });
   }
}; 
