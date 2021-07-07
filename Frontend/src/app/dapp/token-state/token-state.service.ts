import { u } from '@cityofzion/neon-js'
import { BehaviorSubject } from 'rxjs';
import { NeoLineN3Interface } from 'src/app/neo-line/neo-line-n3-interface';

export class TokenStateService {

  Id: string;
  Owner = new BehaviorSubject('');
  Index: string;

  Name: string;
  Description: string;
  Category: string;
  Collection: string;
  Image: string;
  LockedContent: string;

  IsListed: boolean;
  Price: Number;
  AcctualPrice: string;
  PurchaseType: string;
  ListedType: string;

  constructor(neoLine: NeoLineN3Interface, data: any = null) {
    this.Id = data[0].value;
    this.Index = data[1].value;
    neoLine.ScriptHashToAddress({ scriptHash: u.reverseHex(u.base642hex(data[2].value)) }).then(e => this.Owner.next(e.address));

    var tokenProperties = data[3].value[0];
    var marketProperties = data[3].value[1];
    
    const lockedContent = tokenProperties.value[5].value;

    this.Name = atob(tokenProperties.value[0].value);
    this.Description = atob(tokenProperties.value[1].value);
    this.Category = atob(tokenProperties.value[2].value);
    this.Collection = atob(tokenProperties.value[3].value);
    this.Image = atob(tokenProperties.value[4].value);
    this.LockedContent =  (lockedContent === undefined ? "" : atob(lockedContent));

    if(marketProperties.value !== undefined)
    {
      this.IsListed = true;
      this.Price = parseFloat(marketProperties.value[0].value)/100000000;
      this.AcctualPrice = marketProperties.value[0].value;
      this.ListedType = atob(marketProperties.value[1].value);
      this.PurchaseType = atob(marketProperties.value[2].value);
    }
   }
}