import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SpendingQueryComponent } from './spending-query.component';

describe('SpendingQueryComponent', () => {
  let component: SpendingQueryComponent;
  let fixture: ComponentFixture<SpendingQueryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SpendingQueryComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SpendingQueryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
