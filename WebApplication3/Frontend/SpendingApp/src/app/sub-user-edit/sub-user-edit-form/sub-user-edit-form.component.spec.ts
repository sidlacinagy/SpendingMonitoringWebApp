import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SubUserEditFormComponent } from './sub-user-edit-form.component';

describe('SubUserEditFormComponent', () => {
  let component: SubUserEditFormComponent;
  let fixture: ComponentFixture<SubUserEditFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SubUserEditFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SubUserEditFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
