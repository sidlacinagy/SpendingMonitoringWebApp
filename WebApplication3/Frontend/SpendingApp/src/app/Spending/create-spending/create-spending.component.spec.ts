import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateSpendingComponent } from './create-spending.component';

describe('CreateSpendingComponent', () => {
  let component: CreateSpendingComponent;
  let fixture: ComponentFixture<CreateSpendingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateSpendingComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateSpendingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
