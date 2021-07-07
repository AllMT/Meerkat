import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CategoryComponent } from './components/category/category.component';
import { MyTokensComponent } from './components/dashboard/my-tokens/my-tokens.component';
import { HomeComponent } from './components/home/home.component';
import { NftDataComponent } from './components/nft-data/nft-data.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'dashboard/tokens', component: MyTokensComponent },
  { path: 'nft/:id', component: NftDataComponent},
  { path: 'category/:category', component: CategoryComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
