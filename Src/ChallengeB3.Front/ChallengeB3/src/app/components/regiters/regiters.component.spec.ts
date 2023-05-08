import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RegitersComponent } from './regiters.component';

describe('RegitersComponent', () => {
  let component: RegitersComponent;
  let fixture: ComponentFixture<RegitersComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RegitersComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RegitersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
