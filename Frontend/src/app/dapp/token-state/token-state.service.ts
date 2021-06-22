import { u } from '@cityofzion/neon-js'
import { NeoLineN3Interface } from 'src/app/neo-line/neo-line-n3-interface';

export class TokenStateService {

  Id: string;
  Owner: string;
  Name: string;
  Description: string;
  Category: string;
  Collection: string;
  Image: string;
  TokenURI: string;
  LockedContent: string;

  

  constructor(neoLine: NeoLineN3Interface, data: any = null) {

    this.Id = data[0].value;
    neoLine.ScriptHashToAddress({ scriptHash: u.reverseHex(u.base642hex(data[1].value)) }).then(e => this.Owner = e.address);
    // console.log(data[2].value[0].value);

    // const lockedContent = data[1].value[0].value[6].value;

    // console.log("name: " + atob(data[1].value[0].value[0].value));
    // console.log("description: " + atob(data[1].value[0].value[1].value));
    // console.log("category: " + atob(data[1].value[0].value[2].value));
    // console.log("collection: " + atob(data[1].value[0].value[3].value));
    // console.log("image: " + atob(data[1].value[0].value[4].value));
    // console.log("tokenUri: " + atob(data[1].value[0].value[5].value));
    // console.log("lockedContent: " + (lockedContent === undefined ? "" : atob(lockedContent)));

    //console.log(this.Id + " " + this.Owner);
   }
}