import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewSpendingComponent } from './view-spending.component';

describe('ViewSpendingComponent', () => {
  let component: ViewSpendingComponent;
  let fixture: ComponentFixture<ViewSpendingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ViewSpendingComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewSpendingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
