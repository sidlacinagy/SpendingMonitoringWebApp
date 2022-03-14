import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteSpendingComponent } from './delete-spending.component';

describe('DeleteSpendingComponent', () => {
  let component: DeleteSpendingComponent;
  let fixture: ComponentFixture<DeleteSpendingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DeleteSpendingComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DeleteSpendingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
