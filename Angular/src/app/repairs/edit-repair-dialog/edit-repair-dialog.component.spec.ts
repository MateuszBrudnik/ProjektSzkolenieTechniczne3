import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditRepairDialogComponent } from './edit-repair-dialog.component';

describe('EditRepairDialogComponent', () => {
  let component: EditRepairDialogComponent;
  let fixture: ComponentFixture<EditRepairDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditRepairDialogComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EditRepairDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
