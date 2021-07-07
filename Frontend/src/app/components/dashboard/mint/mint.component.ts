import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MintDialogComponent } from '../mint-dialog/mint-dialog.component';

@Component({
  selector: 'app-mint',
  templateUrl: './mint.component.html',
  styleUrls: ['./mint.component.less']
})
export class MintComponent implements OnInit {

  constructor(public dialog: MatDialog) { }

  ngOnInit(): void {
  }

  newToken()
  {
    this.dialog.open(MintDialogComponent);
  }
}
