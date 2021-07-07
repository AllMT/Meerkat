import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { T3Service } from 'src/app/dapp/t3/t3.service';
import { MintDialogComponent } from '../../dashboard/mint-dialog/mint-dialog.component';

@Component({
  selector: 'app-top-nav',
  templateUrl: './top-nav.component.html',
  styleUrls: ['./top-nav.component.less']
})
export class TopNavComponent implements OnInit {

  address = "";

  constructor(private snackBar: MatSnackBar,
    private route: Router,
    private dialog: MatDialog,
    private T3: T3Service) { }

  ngOnInit(): void {
    this.T3.ACCOUNT_ADDRESS
    .subscribe(data => this.address = data);
  }

  comingSoon()
  {
    this.snackBar.open("Coming soon!!", 
    '', 
    { 
      duration: 2000
    });
  }

  myTokens()
  {
    this.route.navigate(['dashboard/tokens'])
  }

  newToken()
  {
    this.dialog.open(MintDialogComponent);
  }
}
