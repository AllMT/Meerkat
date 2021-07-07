import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';
import { NftComponent } from './components/nft/nft.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialsModule } from './materials';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RowComponent } from './components/nft/row/row.component';
import { ColumnComponent } from './components/nft/column/column.component';
import { CategoriesComponent } from './components/category/categories/categories.component';
import { TopNavComponent } from './components/home/top-nav/top-nav.component';
import { LoadingComponent } from './components/loading/loading.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { MintDialogComponent } from './components/dashboard/mint-dialog/mint-dialog.component';
import { MintComponent } from './components/dashboard/mint/mint.component';
import { MyTokensComponent } from './components/dashboard/my-tokens/my-tokens.component';
import { NftDataComponent } from './components/nft-data/nft-data.component';
import { ListTokenComponent } from './components/nft-data/list-token/list-token.component';
import { CategoryComponent } from './components/category/category.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NftComponent,
    RowComponent,
    ColumnComponent,
    CategoriesComponent,
    TopNavComponent,
    LoadingComponent,
    DashboardComponent,
    MintDialogComponent,
    MintComponent,
    MyTokensComponent,
    NftDataComponent,
    ListTokenComponent,
    CategoryComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MaterialsModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
