import { TestBed } from '@angular/core/testing';

import { RegistersService } from './registers.service';

describe('RegistersService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: RegistersService = TestBed.get(RegistersService);
    expect(service).toBeTruthy();
  });
});
