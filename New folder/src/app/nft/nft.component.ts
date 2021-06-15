import { Component, OnInit } from '@angular/core';
import NeoLineN3Init from '../neo-line/neo-line-n3-init';
import { NeoLineN3Interface } from '../neo-line/neo-line-n3-interface';

@Component({
  selector: 'app-nft',
  templateUrl: './nft.component.html',
  styleUrls: ['./nft.component.less']
})
export class NFTComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {

    window.addEventListener("NEOLine.NEO.EVENT.READY", async () => {
      var neoLine = await NeoLineN3Init;

      (await neoLine()).getAccount().then(e => console.log(e));

      //      NEOLineN3.N3.invokeRead({scriptHash: '0x54a098fe55838948173ce7dc4ac877a2c4f87bbd', operation: 'symbol', args:[],signers:[]}).then(e => console.log(atob(e.stack[0].value)))


      // await PetShopContract.updateContractState(neoLine, setContractState);
      // window.addEventListener("NEOLine.NEO.EVENT.BLOCK_HEIGHT_CHANGED", () =>
      //   PetShopContract.updateContractState(neoLine, setContractState)
      // );
    });
  }

}
