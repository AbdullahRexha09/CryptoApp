import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FavoritecryptosComponent } from './favoritecryptos.component';

describe('FavoritecryptosComponent', () => {
  let component: FavoritecryptosComponent;
  let fixture: ComponentFixture<FavoritecryptosComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FavoritecryptosComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FavoritecryptosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
