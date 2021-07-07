import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { T3Service } from 'src/app/dapp/t3/t3.service';

@Component({
  selector: 'app-list-token',
  templateUrl: './list-token.component.html',
  styleUrls: ['./list-token.component.less']
})
export class ListTokenComponent implements OnInit {
  
  token = this.fb.group({
    purchaseType: ['Gas'],
    listingType: ['Sale'],
    price: ['']
  });

  constructor(private fb: FormBuilder, 
    private T3: T3Service, 
    private dialogRef: MatDialogRef<ListTokenComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) { }

  ngOnInit(): void {
  }


  list()
  {
    this.token.patchValue({price: (this.token.value['price'] * 100000000) .toString()});
    this.T3.listToken(this.data, JSON.stringify(this.token.value))
    .then(e => this.dialogRef.close())
  }

}
