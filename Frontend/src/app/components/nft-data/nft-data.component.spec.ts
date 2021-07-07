import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NftDataComponent } from './nft-data.component';

describe('NftDataComponent', () => {
  let component: NftDataComponent;
  let fixture: ComponentFixture<NftDataComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NftDataComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NftDataComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
