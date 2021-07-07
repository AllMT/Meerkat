import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.less']
})
export class CategoriesComponent implements OnInit {

  categories = ['Art', 'Music', 'Domain names', 'Virtual worls', 'Trading cards', 'Collectibles', 'Sports', 'Utility', 'Latest NFTs'];

  categoryObjects = 
  [
    { name:'Art',url:'category/art' },
    { name:'Music',url:'category/music' },
    { name:'Domain names',url:'category/domainnames' },
    { name:'Virtual worls',url:'category/virtualworls' },
    { name:'Trading cards',url:'category/tradingcards' },
    { name:'Collectibles',url:'category/collectibles' },
    { name:'Sports',url:'category/sports' },
    { name:'Utility',url:'category/utility' },
    { name:'Latest NFTs',url:'category/latest' },
  ]

  constructor() { }

  ngOnInit(): void {
  }

}
