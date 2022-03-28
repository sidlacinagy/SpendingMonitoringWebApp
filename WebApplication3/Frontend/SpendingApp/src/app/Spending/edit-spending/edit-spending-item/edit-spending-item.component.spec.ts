import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditSpendingItemComponent } from './edit-spending-item.component';

describe('EditSpendingItemComponent', () => {
  let component: EditSpendingItemComponent;
  let fixture: ComponentFixture<EditSpendingItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditSpendingItemComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EditSpendingItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
