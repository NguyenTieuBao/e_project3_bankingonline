import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StaffManagerAccountManagementComponent } from './staff-manager-account-management.component';

describe('StaffManagerAccountManagementComponent', () => {
  let component: StaffManagerAccountManagementComponent;
  let fixture: ComponentFixture<StaffManagerAccountManagementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [StaffManagerAccountManagementComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(StaffManagerAccountManagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
