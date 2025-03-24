import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomerHelpRequestComponent } from './customer-help-request.component';

describe('CustomerHelpRequestComponent', () => {
  let component: CustomerHelpRequestComponent;
  let fixture: ComponentFixture<CustomerHelpRequestComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CustomerHelpRequestComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CustomerHelpRequestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
