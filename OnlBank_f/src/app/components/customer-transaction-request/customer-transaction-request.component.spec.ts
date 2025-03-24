import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomerTransactionRequestComponent } from './customer-transaction-request.component';

describe('CustomerTransactionRequestComponent', () => {
  let component: CustomerTransactionRequestComponent;
  let fixture: ComponentFixture<CustomerTransactionRequestComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CustomerTransactionRequestComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CustomerTransactionRequestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
