import { TestBed } from '@angular/core/testing';

import { T3Service } from './t3.service';

describe('T3Service', () => {
  let service: T3Service;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(T3Service);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
