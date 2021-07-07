import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { T3Service } from 'src/app/dapp/t3/t3.service';
import { TokenStateService } from 'src/app/dapp/token-state/token-state.service';
import { ListTokenComponent } from './list-token/list-token.component';

@Component({
  selector: 'app-nft-data',
  templateUrl: './nft-data.component.html',
  styleUrls: ['./nft-data.component.less']
})
export class NftDataComponent implements OnInit {

  token: TokenStateService;
  categoryUrl: string;
  isMine: boolean;

  constructor(private T3: T3Service, 
    private route: ActivatedRoute,
    private router: Router,
    private snackBar: MatSnackBar,
    public dialog: MatDialog) { }

  ngOnInit(): void {

    this.route.params.subscribe(parameter => {

       this.T3.walletInitalized.subscribe(data =>
        {
          if(data)
          {
            this.T3.getTokenByIndexFromContract(parameter.id)
            .then(result => 
              {
                if(result)
                {
                  this.token = result;
                  this.categoryUrl = "category/" + this.token.Category.toLocaleLowerCase();

                  this.token.Owner.subscribe(data =>
                  {
                    this.isMine = data === this.T3.ACCOUNT_ADDRESS.value;
                  });
                }
              });
          }
        })
    })
  }

  burn()
  {
    this.T3.burn(this.token.Id)
    .then(e =>
      {
        this.snackBar.open("Successfull, token will be burned!!", '', {duration: 3000})
        this.router.navigate(['/']);
      });
  }

  list()
  {
    this.dialog.open(ListTokenComponent, { data: this.token.Id });
  }

  buy()
  {
    this.T3.purchase(this.token.Id, this.token.AcctualPrice);
  }

  removeListing()
  {
    this.T3.removeListing(this.token.Id)
    .then(e => 
      {
        this.snackBar.open("Successfully removed the listing", '', {duration: 3000})
    });
  }
}
