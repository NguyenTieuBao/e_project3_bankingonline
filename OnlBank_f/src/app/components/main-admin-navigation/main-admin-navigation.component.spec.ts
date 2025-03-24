import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MainAdminNavigationComponent } from './main-admin-navigation.component';

describe('MainAdminNavigationComponent', () => {
  let component: MainAdminNavigationComponent;
  let fixture: ComponentFixture<MainAdminNavigationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [MainAdminNavigationComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(MainAdminNavigationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
