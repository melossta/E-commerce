import { TestBed } from '@angular/core/testing';

import { ShippingDetailsService } from './shipping-details.service';

describe('ShippingDetailsService', () => {
  let service: ShippingDetailsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ShippingDetailsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
