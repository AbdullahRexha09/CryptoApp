import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CryptoselectComponent } from './cryptoselect.component';

describe('CryptoselectComponent', () => {
  let component: CryptoselectComponent;
  let fixture: ComponentFixture<CryptoselectComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CryptoselectComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CryptoselectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
