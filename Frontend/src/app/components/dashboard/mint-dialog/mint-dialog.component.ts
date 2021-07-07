import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { T3Service } from 'src/app/dapp/t3/t3.service';

@Component({
  selector: 'app-mint-dialog',
  templateUrl: './mint-dialog.component.html',
  styleUrls: ['./mint-dialog.component.less']
})
export class MintDialogComponent implements OnInit {

  scriptHash: "";

  token = this.fb.group({
    name: [''],
    description: [''],
    image: [''],
    lockedContent: [''],
    category: ['Art'],
    collection: ['']
  });

  constructor(private fb: FormBuilder, 
    private t3: T3Service,
    private dialogRef: MatDialogRef<MintDialogComponent>,
    private snackBar: MatSnackBar) { }

  ngOnInit(): void {
  }

  mintNewToken()
  {
    this.t3.mint(JSON.stringify(this.token.value))
    .then(e => {
      this.dialogRef.close();
      //this.snackBar.open("The operation was successfully" + "<a href='https://neo3.neotube.io/transaction/" + e.txid + "'>"+ e.txid + "</a>");
      this.snackBar.open("The operation was successfully", 
      '', 
      { 
        duration: 2000
      });
    });
  }
}
